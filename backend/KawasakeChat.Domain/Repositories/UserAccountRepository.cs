using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Domain.Repositories;

public class UserAccountRepository(KawasakeChatDbContext kawasakeChatDbContext) : IUserAccountRepository
{
    private readonly KawasakeChatDbContext  _ctx = kawasakeChatDbContext;
    
    public async Task<Useraccount> CreateUserAccount(Useraccount userAccount)
    {
        try
        {
            await _ctx.Useraccounts.AddAsync(userAccount);
            await _ctx.SaveChangesAsync();

            return userAccount;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Useraccount? GetUserAccount(string username)
    {
        try
        {
            return _ctx.Useraccounts.FirstOrDefault(usra => usra.Username == username && usra.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Useraccount? GetUserAccount(Guid userId)
    {
        try
        {
            return _ctx.Useraccounts.FirstOrDefault(usra => usra.UserId == userId && usra.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IQueryable<Useraccount> GetUserAccounts()
    {
        try
        {
            return _ctx.Useraccounts.Where(usra => usra.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}