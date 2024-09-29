using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Models.Requests;
using KawasakeChat.WebAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccountController(IUserAccountService userAccountService) : ControllerBase, IUserAccountController
{
    private readonly IUserAccountService _srvUserAccount = userAccountService;
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateUserAccount(UserAccountCreateRequest request)
    {
        try
        {
            var srvData = await _srvUserAccount.CreateUserAccount(request);
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = _srvUserAccount.Me(userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{username}")]
    [Authorize]
    public async Task<IActionResult> GetUserAccount(string username)
    {
        try
        {
            var srvData = _srvUserAccount.GetUserAccount(username);
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "SuperUser, Moderator")]
    public async Task<IActionResult> GetUserAccounts()
    {
        try
        {
            var srvData = _srvUserAccount.GetUserAccounts();
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}