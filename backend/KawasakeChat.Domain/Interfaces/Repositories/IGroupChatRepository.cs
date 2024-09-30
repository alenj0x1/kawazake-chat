using KawasakeChat.Domain.Entities;

namespace KawasakeChat.Domain.Interfaces.Repositories;

public interface IGroupChatRepository
{
    Task<Groupchat> CreateGroupchat(Groupchat groupChat);
    Groupchat? GetGroupChat(Guid groupChatId);
    Groupchat? GetGroupChat(string groupName);
    Groupchat? GetGroupChatByInviteCode(string inviteCode);
    IQueryable<Groupchat> GetGroupChats();
    Task<Groupchat> UpdateGroupChat(Groupchat groupChat);
    Task<bool> DeleteGroupChat(Groupchat groupChat);
    
    // Group chat member
    Task<Groupchatmember> CreateGroupChatMember(Groupchatmember groupChatMember);
    Groupchatmember? GetGroupChatOwner(Guid groupChatId);
    Groupchatmember? GetGroupChatMemberByMemberId(Guid groupChatId, Guid memberId);
    Groupchatmember? GetGroupChatMemberByUserId(Guid groupChatId, Guid userId);
    List<Guid> GetGroupChatMembersIds(Guid groupChatId);
    List<Guid> GetGroupChatsIdsByUserId(Guid userId);
    Task<Groupchatmember> UpdateGroupChatMember(Groupchatmember groupChatMember);
    Task<bool> TransferGroupChatOwnership(Groupchat groupChat, Groupchatmember oldOwnerGroupChatMember, Groupchatmember newOwnerGroupChatMember);
    Task<bool> DeleteGroupChatMember(Groupchatmember groupChatMember);
}