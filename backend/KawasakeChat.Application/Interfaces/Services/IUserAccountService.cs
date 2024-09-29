using System.Security.Claims;
using KawasakeChat.Models.Requests;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Application.Interfaces.Services;

public interface IUserAccountService
{
    Task<UserAccountDto> CreateUserAccount(UserAccountCreateRequest request);
    UserAccountDto GetUserAccount(string username);
    UserAccountDto Me(Claim userId);
    List<UserAccountDto> GetUserAccounts();
}