using System.Text;
using KawasakeChat.Application.Helpers;
using KawasakeChat.Application.Interfaces.Helpers;
using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Application.Services;
using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Domain.Repositories;
using KawasakeChat.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KawasakeChat.WebAPI.Extensions;

public static class ServiceExtension
{
    public static async Task AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<KawasakeChatDbContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("Database") ?? throw new Exception("missing database connection string"));
        });
        
        // Authentication and authorization
        services.AddAuthentication(builder =>
        {
            builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(builder =>
        {
            builder.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"] ?? throw new Exception("missing jwt issuer"),
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"] ?? throw new Exception("missing jwt audience"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"] ?? throw new Exception("jwt secret key is null"))),
                ValidateLifetime = false // <- by default is true
            };
            builder.Events = new JwtBearerEvents()
            {
                // Customized events
                OnTokenValidated = async context =>
                {
                    var securityToken = context.Request.Headers.Authorization.ToString()[7..];
                    if (string.IsNullOrEmpty(securityToken))
                    {
                        context.Fail("token_not_found");
                        return;
                    }
                        
                    var repApp = context.HttpContext.RequestServices.GetRequiredService<ITokenRepository>();
                    
                    var tkAccess = repApp.GetTokenAccess(securityToken); // <- search on database
                    if (tkAccess is null)
                    {
                        context.Fail("token_not_found");
                        return;
                    }

                    if (DateTime.UtcNow.CompareTo(tkAccess.Expiration) >= 0) // <- lifetime validation
                    {
                        context.Fail("token_expired");
                    }
                },
                OnChallenge = async context =>
                {
                    if (!string.IsNullOrWhiteSpace(context.AuthenticateFailure?.Message))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(context.AuthenticateFailure.Message);
                    }
                }
            };
        });

        services.AddAuthorization();
        
        // Mapping
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Services
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGroupChatService, GroupChatService>();
        
        // Repositories
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<IAppRepository, AppRepository>();
        services.AddScoped<IGroupChatRepository, GroupChatRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        
        // Helpers
        services.AddScoped<IToken, Token>();
        
        // First user creation
        var userAccountRepository = services.BuildServiceProvider().GetRequiredService<IUserAccountRepository>();
        if (await Verify.FirstUserCreation(userAccountRepository, configuration))
        {
            Console.WriteLine("First user account created!");
        }
    }
}