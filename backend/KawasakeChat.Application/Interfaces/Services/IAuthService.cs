using KawasakeChat.Models.Requests.Auth;
using KawasakeChat.Models.Responses.Auth;

namespace KawasakeChat.Application.Interfaces.Services;

public interface IAuthService
{
    public Task<LoginResponse?> Login(LoginRequest request);
    public Task<LoginResponse?> RenewAccess(RenewAccessRequest request);
}