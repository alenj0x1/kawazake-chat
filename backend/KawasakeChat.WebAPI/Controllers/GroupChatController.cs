using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Models.Requests;
using KawasakeChat.Models.Requests.GroupChat;
using KawasakeChat.WebAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KawasakeChat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupChatController(IGroupChatService groupChatService) : ControllerBase, IGroupChatController
{
    private readonly IGroupChatService _srvGroupChat = groupChatService; 
    
    [HttpPost("Create")]
    [Authorize]
    public async Task<IActionResult> CreateGroupChat(GroupChatCreateRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.CreateGroupChat(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GetGroupChats(BaseRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = _srvGroupChat.GetGroupChats(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("Join")]
    [Authorize]
    public async Task<IActionResult> JoinToGroupChat([FromBody] GroupChatJoinRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.JoinToGroupChat(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("Leave/{groupChatId:guid}")]
    [Authorize]
    public async Task<IActionResult> LeaveToGroupChat(Guid groupChatId)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.LeaveToGroupChat(groupChatId, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("Update")]
    [Authorize]
    public async Task<IActionResult> UpdateGroupChat(GroupChatUpdateRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.UpdateGroupChat(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordGroupChat(GroupChatChangePasswordRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.ChangePasswordGroupChat(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("TransferOwnership")]
    [Authorize]
    public async Task<IActionResult> TransferOwnershipGroupChat(GroupChatTransferOwnershipRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.TransferOwnershipGroupChat(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("Delete/{groupChatId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteGroupChat(Guid groupChatId)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.DeleteGroupChat(groupChatId, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Administrator
    [HttpPut("Administrator/Update")]
    [Authorize]
    public async Task<IActionResult> UpdateGroupChatMember([FromBody] GroupChatUpdateMemberRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.UpdateGroupChatMember(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("Administrator/Delete")]
    [Authorize]
    public async Task<IActionResult> DeleteGroupChatMember([FromBody] GroupChatDeleteMemberRequest request)
    {
        try
        {
            var userId = User.FindFirst("UserId") ?? throw new Exception("user not found");
            var srvData = await _srvGroupChat.DeleteGroupChatMember(request, userId);
            
            return Ok(srvData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}