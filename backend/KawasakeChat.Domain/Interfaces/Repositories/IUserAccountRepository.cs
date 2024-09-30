using KawasakeChat.Domain.Entities;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Domain.Interfaces.Repositories;

public interface IUserAccountRepository
{
    Task<Useraccount> CreateUserAccount(Useraccount userAccount);
    Useraccount? GetUserAccount(string username);
    Useraccount? GetUserAccount(Guid userId);
    IQueryable<Useraccount> GetUserAccounts();
}