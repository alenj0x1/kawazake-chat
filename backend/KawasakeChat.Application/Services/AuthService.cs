using KawasakeChat.Application.Helpers;
using KawasakeChat.Application.Interfaces.Helpers;
using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Models.Requests.Auth;
using KawasakeChat.Models.Responses.Auth;
using KawasakeChat.Shared;
using Microsoft.Extensions.Configuration;

namespace KawasakeChat.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserAccountRepository _repUserAccount;
    private readonly IAppRepository _repApp;
    private readonly ITokenRepository _repToken;
    private readonly IConfiguration _config;
    private readonly IToken _tkHelper;

    public AuthService(IUserAccountRepository userAccountRepository, IAppRepository appRepository, ITokenRepository tokenRepository, IConfiguration configuration, IToken token)
    {
        _repUserAccount = userAccountRepository;
        _repApp = appRepository;
        _repToken = tokenRepository;
        _config = configuration;
        _tkHelper = new Token(_config, _repApp);
    }
    
    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        try
        {
            var usr = _repUserAccount.GetUserAccount(request.Username) ?? throw new Exception("username or password incorrects");;
            if (!Hasher.Compare(request.Password, usr.Password)) throw new Exception("username or password incorrects");

            return await CreateTokens(usr);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LoginResponse?> RenewAccess(RenewAccessRequest request)
    {
        try
        {
            var tkRefresh = _repToken.GetTokenRefresh(request.RefreshToken) ?? throw new Exception("refresh token could not be retrieved");
            var usr = _repUserAccount.GetUserAccount(tkRefresh.UserId) ?? throw new Exception("user could not be retrieved");
            
            return await CreateTokens(usr);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<LoginResponse> CreateTokens(Useraccount useraccount)
    {
        try
        {
            // Expiration
            var accTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:Expiration"] ?? throw new Exception("jwt expiration is null")));
            
            // Generate Access and Refresh tokens
            var tkAccess = _tkHelper.CreateAccessToken(useraccount, accTokenExpiration) ?? throw new Exception("token could not be created");
            var tkRefresh = Hasher.Hash(Generate.RandomString());
            
            // Save Refresh and Token tokens
            var svTokenRefresh = await _repToken.CreateTokenRefresh(new Tokenrefresh()
            {
                Value = tkRefresh,
                UserId = useraccount.UserId,
                Expiration = null // <- for now
            });
            
            var svTokenAccess = await _repToken.CreateTokenAccess(new Tokenaccess()
            {
                TokenRefreshId = svTokenRefresh.TokenRefreshId, // <- refresh token ID
                UserId = useraccount.UserId,
                Value = tkAccess,
                Expiration = accTokenExpiration
            });

            return new LoginResponse()
            {
                RefreshToken = svTokenRefresh.Value,
                AccessToken  = svTokenAccess.Value,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}