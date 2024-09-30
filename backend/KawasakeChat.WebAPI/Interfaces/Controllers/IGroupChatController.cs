using System.Security.Claims;
using KawasakeChat.Models.Requests;
using KawasakeChat.Models.Requests.GroupChat;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Interfaces.Controllers;

public interface IGroupChatController
{
    Task<IActionResult> CreateGroupChat([FromBody] GroupChatCreateRequest request);
    Task<IActionResult> GetGroupChats([FromBody] BaseRequest request);
    Task<IActionResult> JoinToGroupChat([FromBody] GroupChatJoinRequest request);
    Task<IActionResult> LeaveToGroupChat(Guid groupChatId);
    Task<IActionResult> UpdateGroupChat([FromBody] GroupChatUpdateRequest request);
    Task<IActionResult> ChangePasswordGroupChat([FromBody] GroupChatChangePasswordRequest request);
    Task<IActionResult> TransferOwnershipGroupChat([FromBody] GroupChatTransferOwnershipRequest request);
    Task<IActionResult> DeleteGroupChat(Guid groupChatId);
    
    // Moderation
    Task<IActionResult> UpdateGroupChatMember([FromBody] GroupChatUpdateMemberRequest request);
    Task<IActionResult> DeleteGroupChatMember([FromBody] GroupChatDeleteMemberRequest request);
}