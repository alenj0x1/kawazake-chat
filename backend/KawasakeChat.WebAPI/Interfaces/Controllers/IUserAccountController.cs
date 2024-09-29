using KawasakeChat.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Interfaces.Controllers;

public interface IUserAccountController
{
    Task<IActionResult> CreateUserAccount([FromBody] UserAccountCreateRequest request);
    Task<IActionResult> GetUserAccount(string username);
    Task<IActionResult> GetUserAccounts();
}