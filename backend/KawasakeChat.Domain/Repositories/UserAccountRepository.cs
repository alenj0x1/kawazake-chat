using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Domain.Repositories;

public class UserAccountRepository(KawasakeChatDbContext kawasakeChatDbContext) : IUserAccountRepository
{
    private readonly KawasakeChatDbContext  _ctx = kawasakeChatDbContext;
    
    public async Task<Useraccount> CreateUserAccount(UserAccountCreateDto data)
    {
        try
        {
            var usrAccount = new Useraccount()
            {
                Username = data.Username,
                Password = data.Password,
                Status = data.Status,
                Role = data.Role,
            };
            
            await _ctx.Useraccounts.AddAsync(usrAccount);
            await _ctx.SaveChangesAsync();

            return usrAccount;
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

    public List<Useraccount> GetUserAccounts()
    {
        try
        {
            return [.. _ctx.Useraccounts.Where(usra => usra.DeletedAt == null)];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}