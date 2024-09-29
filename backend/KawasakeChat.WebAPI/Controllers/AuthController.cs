using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Models.Requests.Auth;
using KawasakeChat.WebAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase, IAuthController
{
    private readonly IAuthService _srvAuth = authService; 
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            return Ok(await _srvAuth.Login(request));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("RenewAccess")]
    public async Task<IActionResult> RenewAccess(RenewAccessRequest request)
    {
        try
        {
            return Ok(await _srvAuth.RenewAccess(request));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}