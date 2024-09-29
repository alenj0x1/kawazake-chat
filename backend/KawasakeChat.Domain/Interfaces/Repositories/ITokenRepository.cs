using KawasakeChat.Domain.Entities;

namespace KawasakeChat.Domain.Interfaces.Repositories;

public interface ITokenRepository
{
    Task<Tokenaccess> CreateTokenAccess(Tokenaccess tokenAccess);
    Task<Tokenrefresh> CreateTokenRefresh(Tokenrefresh tokenRefresh);
    Tokenaccess? GetTokenAccess(string value);
    Tokenaccess? GetTokenAccess(Guid userId);
    Tokenrefresh? GetTokenRefresh(string value);
    Tokenrefresh? GetTokenRefresh(Guid userId);
    Task<bool> DeleteTokenAccess(Tokenaccess tokenAccess);
    Task<bool> DeleteTokenRefresh(Tokenrefresh tokenrefresh);
}