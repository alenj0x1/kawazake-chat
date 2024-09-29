using System.Security.Claims;
using AutoMapper;
using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Models.Requests;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Dto.UserAccount;
using KawasakeChat.Shared;

namespace KawasakeChat.Application.Services;

public class UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository) : IUserAccountService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserAccountRepository _repUserAccount = userAccountRepository;
    
    public async Task<UserAccountDto> CreateUserAccount(UserAccountCreateRequest request)
    {
        try
        {
            var gtUserAccount = _repUserAccount.GetUserAccount(request.Username);
            if (gtUserAccount is not null) throw new ArgumentException("Username already exists");

            var crtUserAccountDto = _mapper.Map<UserAccountCreateDto>(request);
            crtUserAccountDto.Password = Hasher.Hash(request.Password);
            
            var data = await _repUserAccount.CreateUserAccount(crtUserAccountDto);
            return _mapper.Map<UserAccountDto>(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public UserAccountDto GetUserAccount(string username)
    {
        try
        {
            var data = _repUserAccount.GetUserAccount(username);
            return _mapper.Map<UserAccountDto>(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public UserAccountDto Me(Claim userIdClaim)
    {
        try
        {
            var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user not found");
            var data = _repUserAccount.GetUserAccount(userId);
            
            return _mapper.Map<UserAccountDto>(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<UserAccountDto> GetUserAccounts()
    {
        try
        {
            var data = _repUserAccount.GetUserAccounts();
            return _mapper.Map <List<UserAccountDto>>(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}