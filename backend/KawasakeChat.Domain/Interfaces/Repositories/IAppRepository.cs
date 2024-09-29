using KawasakeChat.Domain.Entities;

namespace KawasakeChat.Domain.Interfaces.Repositories;

public interface IAppRepository
{
    List<Useraccountrole> GetUserAccountRoles();
    Useraccountrole? GetUserAccountRole(int roleId);
}