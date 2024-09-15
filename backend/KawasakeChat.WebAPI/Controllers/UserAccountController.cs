using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Application.Models.Requests;
using KawasakeChat.WebAPI.Interfaces.Controllers;
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
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserAccount(string username)
    {
        try
        {
            var srvData = _srvUserAccount.GetUserAccount(username);
            return Ok(srvData);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAccounts()
    {
        try
        {
            var srvData = _srvUserAccount.GetUserAccounts();
            return Ok(srvData);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}