using System.Security.Claims;
using KawasakeChat.Application.Models.Helpers.Verify;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Shared;
using Microsoft.Extensions.Configuration;

namespace KawasakeChat.Application.Helpers;

public static class Verify
{
    public static MemberPermissionsData MemberPermissions(IGroupChatRepository groupChatRepository, Guid groupChatId, Claim userIdClaim, int role = Consts.RoleGroupChatMemberAdministrator)
    {
        try
        {
            var grpChat = groupChatRepository.GetGroupChat(groupChatId) ?? throw new Exception("group chat does not exist");
            
            var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user does not exist");
            var grpChatMember = groupChatRepository.GetGroupChatMemberByUserId(grpChat.GroupChatId, userId) ?? throw new Exception("the user does not belong to the group");

            if (grpChatMember.Role > role) throw new Exception("the user is not an administrator");

            return new MemberPermissionsData()
            {
                GroupChat = grpChat,
                GroupChatMember = grpChatMember
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<bool> FirstUserCreation(IUserAccountRepository userAccountRepository, IConfiguration configuration)
    {
        try
        {
            if (userAccountRepository.GetUserAccounts().ToList().Count > 0) return false;

            var password = configuration["FirstUser:Password"] ?? throw new Exception("first user password is missing");
            await userAccountRepository.CreateUserAccount(new Useraccount()
            {
                Username = configuration["FirstUser:Username"] ?? throw new Exception("first user username is missing"),
                Password = Hasher.Hash(password),
                Role = Consts.RoleUserAccountSuperUser
            });
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}