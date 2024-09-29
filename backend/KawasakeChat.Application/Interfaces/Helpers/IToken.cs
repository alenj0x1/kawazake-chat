using KawasakeChat.Domain.Entities;

namespace KawasakeChat.Application.Interfaces.Helpers;

public interface IToken
{
    string? CreateAccessToken(Useraccount userAccount, DateTime expiration);
    string CreateRefreshToken(Useraccount userAccount);
}