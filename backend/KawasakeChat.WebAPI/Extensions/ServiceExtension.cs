using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Application.Services;
using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Domain.Repositories;
using KawasakeChat.Mapping;
using Microsoft.EntityFrameworkCore;

namespace KawasakeChat.WebAPI.Extensions;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<KawasakeChatDbContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("Database") ?? throw new Exception("Missing 'Database' connection string"));
        });

        // Mapping
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Services
        services.AddScoped<IUserAccountService, UserAccountService>();
        
        // Repositories
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
    }
}