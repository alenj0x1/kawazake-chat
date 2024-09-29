using KawasakeChat.Models.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Interfaces.Controllers;

public interface IAuthController
{
    Task<IActionResult> Login(LoginRequest request);
    Task<IActionResult> RenewAccess(RenewAccessRequest request);
}