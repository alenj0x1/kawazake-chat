using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;

namespace KawasakeChat.Domain.Repositories;

public class TokenRepository(KawasakeChatDbContext kawasakeChatDbContext) : ITokenRepository
{
    private readonly KawasakeChatDbContext  _ctx = kawasakeChatDbContext;
    
    public async Task<Tokenaccess> CreateTokenAccess(Tokenaccess tokenAccess)
    {
        try
        {
            var gtTokenAccess = GetTokenAccess(tokenAccess.UserId);
            if (gtTokenAccess is not null)
            {
                var delTokenAccess = await DeleteTokenAccess(gtTokenAccess);
                if (!delTokenAccess) throw new Exception("previously token access not was deleted");
            }
            
            await _ctx.Tokenaccesses.AddAsync(tokenAccess);
            await _ctx.SaveChangesAsync();
            
            return tokenAccess;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Tokenrefresh> CreateTokenRefresh(Tokenrefresh tokenRefresh)
    {
        try
        {
            var gtTokenRefresh = GetTokenRefresh(tokenRefresh.UserId);
            if (gtTokenRefresh is not null)
            {
                var delTokenRefresh = await DeleteTokenRefresh(gtTokenRefresh);
                if (!delTokenRefresh) throw new Exception("previously token refresh not was deleted");
            }
            
            await _ctx.Tokenrefreshes.AddAsync(tokenRefresh);
            await _ctx.SaveChangesAsync();
            
            return tokenRefresh;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Tokenaccess? GetTokenAccess(string value)
    {
        try
        {
            return _ctx.Tokenaccesses.FirstOrDefault(tkacc => tkacc.Value == value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public Tokenaccess? GetTokenAccess(Guid userId)
    {
        try
        {
            return _ctx.Tokenaccesses.FirstOrDefault(tkacc => tkacc.UserId == userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Tokenrefresh? GetTokenRefresh(string value)
    {
        try
        {
            return _ctx.Tokenrefreshes.FirstOrDefault(tkrf => tkrf.Value == value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Tokenrefresh? GetTokenRefresh(Guid userId)
    {
        try
        {
            return _ctx.Tokenrefreshes.FirstOrDefault(tkrf => tkrf.UserId == userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteTokenAccess(Tokenaccess tokenAccess)
    {
        try
        {
            _ctx.Tokenaccesses.Remove(tokenAccess);
            var result = await _ctx.SaveChangesAsync();
            
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteTokenRefresh(Tokenrefresh tokenrefresh)
    {
        try
        {
            _ctx.Tokenrefreshes.Remove(tokenrefresh);
            var result = await _ctx.SaveChangesAsync();
            
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}