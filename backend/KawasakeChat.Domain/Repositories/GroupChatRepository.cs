using KawasakeChat.Domain.Context;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;

namespace KawasakeChat.Domain.Repositories;

public class GroupChatRepository(KawasakeChatDbContext kawasakeChatDbContext) : IGroupChatRepository
{
    private readonly KawasakeChatDbContext  _ctx = kawasakeChatDbContext;
    
    public async Task<Groupchat> CreateGroupchat(Groupchat groupChat)
    {
        try
        {
            _ctx.Groupchats.Add(groupChat);
            await _ctx.SaveChangesAsync();
            
            return groupChat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchat? GetGroupChat(Guid groupChatId)
    {
        try
        {
            return _ctx.Groupchats.FirstOrDefault(grpc => grpc.GroupChatId == groupChatId && grpc.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchat? GetGroupChat(string groupName)
    {
        try
        {
            return _ctx.Groupchats.FirstOrDefault(grpc => grpc.Name == groupName && grpc.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchat? GetGroupChatByInviteCode(string inviteCode)
    {
        try
        {
            return _ctx.Groupchats.FirstOrDefault(grpc => grpc.InviteCode == inviteCode && grpc.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IQueryable<Groupchat> GetGroupChats()
    {
        try
        {
            return _ctx.Groupchats.Where(grpc => grpc.DeletedAt == null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Groupchat> UpdateGroupChat(Groupchat groupChat)
    {
        try
        {
            _ctx.Groupchats.Update(groupChat);
            await _ctx.SaveChangesAsync();

            return groupChat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteGroupChat(Groupchat groupChat)
    {
        try
        {
            await DeleteGroupChatMembers(groupChat.GroupChatId); // <- delete all members for this group chat

            groupChat.Name = groupChat.GroupChatId + "_deleted";
            groupChat.DeletedAt = DateTime.UtcNow;;
            
            _ctx.Groupchats.Update(groupChat);
            
            var result = await _ctx.SaveChangesAsync();
            
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private async Task<bool> DeleteGroupChatMembers(Guid groupChatId)
    {
        try
        {
            foreach (var grpcm in _ctx.Groupchatmembers.Where(grpcm => grpcm.GroupChatId == groupChatId))
            {
                _ctx.Groupchatmembers.Remove(grpcm);
            }
            
            var result = await _ctx.SaveChangesAsync();
            
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Group chat member
    public async Task<Groupchatmember> CreateGroupChatMember(Groupchatmember groupChatMember)
    {
        try
        {
            await _ctx.Groupchatmembers.AddAsync(groupChatMember);
            await _ctx.SaveChangesAsync();
            
            return groupChatMember;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchatmember? GetGroupChatOwner(Guid groupChatId)
    {
        try
        {
            var grpChat = GetGroupChat(groupChatId);
            return grpChat is not null ? _ctx.Groupchatmembers.FirstOrDefault(grpcm => grpcm.GroupChatId == groupChatId && grpcm.UserId == grpChat.OwnerId) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchatmember? GetGroupChatMemberByMemberId(Guid groupChatId, Guid memberId)
    {
        try
        {
            return _ctx.Groupchatmembers.FirstOrDefault(grpcm => grpcm.GroupChatId == groupChatId && grpcm.MemberId == memberId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Groupchatmember? GetGroupChatMemberByUserId(Guid groupChatId, Guid userId)
    {
        try
        {
            return _ctx.Groupchatmembers.FirstOrDefault(grpcm => grpcm.GroupChatId == groupChatId && grpcm.UserId == userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Guid> GetGroupChatMembersIds(Guid groupChatId)
    {
        try
        {
            return [.. _ctx.Groupchatmembers.Where(grpcm => grpcm.GroupChatId == groupChatId).Select(grpcm => grpcm.UserId)];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Guid> GetGroupChatsIdsByUserId(Guid userId)
    {
        try
        {
            return [.. _ctx.Groupchatmembers.Where(grpcm => grpcm.UserId == userId).Select(grpcm => grpcm.UserId)];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Groupchatmember> UpdateGroupChatMember(Groupchatmember groupChatMember)
    {
        try
        {
            _ctx.Groupchatmembers.Update(groupChatMember);
            await _ctx.SaveChangesAsync();
            
            return groupChatMember;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> TransferGroupChatOwnership(Groupchat groupChat, Groupchatmember oldOwnerGroupChatMember, Groupchatmember newOwnerGroupChatMember)
    {
        try
        {
            _ctx.Groupchats.Update(groupChat);
            
            _ctx.Groupchatmembers.Update(oldOwnerGroupChatMember);
            _ctx.Groupchatmembers.Update(newOwnerGroupChatMember);
            
            var result = await _ctx.SaveChangesAsync();

            return result == 3;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteGroupChatMember(Groupchatmember groupChatMember)
    {
        try
        {
            _ctx.Groupchatmembers.Remove(groupChatMember);
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