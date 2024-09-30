using System.Security.Claims;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Dto.GroupChat;
using KawasakeChat.Models.Requests;
using KawasakeChat.Models.Requests.GroupChat;

namespace KawasakeChat.Application.Interfaces.Services;

public interface IGroupChatService
{
    Task<GroupChatDto> CreateGroupChat(GroupChatCreateRequest request, Claim userIdClaim);
    List<GroupChatDto> GetGroupChats(BaseRequest request, Claim userIdClaim);
    Task<GroupChatDto> UpdateGroupChat(GroupChatUpdateRequest request, Claim userIdClaim);
    Task<bool> JoinToGroupChat(GroupChatJoinRequest request, Claim userIdClaim);
    Task<bool> LeaveToGroupChat(Guid groupChatId, Claim userIdClaim);
    Task<GroupChatDto> TransferOwnershipGroupChat(GroupChatTransferOwnershipRequest request, Claim userIdClaim);
    Task<GroupChatDto> ChangePasswordGroupChat(GroupChatChangePasswordRequest request, Claim userIdClaim);
    Task<bool> DeleteGroupChat(Guid groupChatId, Claim userIdClaim);
    
    // Administrator
    Task<bool> UpdateGroupChatMember(GroupChatUpdateMemberRequest request, Claim userIdClaim);
    Task<bool> DeleteGroupChatMember(GroupChatDeleteMemberRequest request, Claim userIdClaim);
}