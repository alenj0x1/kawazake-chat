using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;

namespace KawasakeChat.Domain.Repositories;

public class AppRepository(KawasakeChatDbContext kawasakeChatDbContext) : IAppRepository
{
    private readonly KawasakeChatDbContext  _ctx = kawasakeChatDbContext;
    
    public List<Useraccountrole> GetUserAccountRoles()
    {
        try
        {
            return [.. _ctx.Useraccountroles];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Useraccountrole? GetUserAccountRole(int roleId)
    {
        try
        {
            return _ctx.Useraccountroles.FirstOrDefault(usrar => usrar.RoleId == roleId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}