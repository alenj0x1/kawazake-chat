using KawasakeChat.Application.Models.Requests;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Application.Interfaces.Services;

public interface IUserAccountService
{
    Task<UserAccountDto> CreateUserAccount(UserAccountCreateRequest request);
    UserAccountDto GetUserAccount(string username);
    List<UserAccountDto> GetUserAccounts();
}