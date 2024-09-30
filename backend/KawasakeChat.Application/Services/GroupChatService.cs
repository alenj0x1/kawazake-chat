using System.Security.Claims;
using AutoMapper;
using KawasakeChat.Application.Helpers;
using KawasakeChat.Application.Interfaces.Services;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using KawasakeChat.Dto.GroupChat;
using KawasakeChat.Models.Requests;
using KawasakeChat.Models.Requests.GroupChat;
using KawasakeChat.Shared;

namespace KawasakeChat.Application.Services;

public class GroupChatService(IUserAccountRepository userAccountRepository,IGroupChatRepository groupChatRepository, IMapper mapper) : IGroupChatService
{
    private readonly IUserAccountRepository _repUserAccount = userAccountRepository;
    private readonly IGroupChatRepository _repGroupChat = groupChatRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<GroupChatDto> CreateGroupChat(GroupChatCreateRequest request, Claim userIdClaim)
    {
        var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user not found");
        var usrAccount = _repUserAccount.GetUserAccount(userId) ?? throw new Exception("user not found");
        
        if (_repGroupChat.GetGroupChat(request.Name) is not null) throw new Exception("name taked");
        
        if (request.Private && string.IsNullOrWhiteSpace(request.Password)) throw new Exception("when a group chat is private, password is required");
        
        var crtGroupChat = await _repGroupChat.CreateGroupchat(new Groupchat()
        {
            Name = request.Name,
            OwnerId = usrAccount.UserId,
            AvatarUrl = request.AvatarUrl,
            InviteCode = Generate.InviteCode(Parser.WithoutWhiteSpaces(request.Name)),
            Private = request.Private,
            Password = string.IsNullOrWhiteSpace(request.Password) ? null : Hasher.Hash(request.Password)
        });

        await _repGroupChat.CreateGroupChatMember(new Groupchatmember()
        {
            GroupChatId = crtGroupChat.GroupChatId,
            UserId = usrAccount.UserId,
            AvatarUrl = usrAccount.AvatarUrl,
            Role = Consts.RoleGroupChatMemberOwner,
        });
        
        var mapped = _mapper.Map<GroupChatDto>(crtGroupChat);
        mapped.Members = _repGroupChat.GetGroupChatMembersIds(crtGroupChat.GroupChatId);
        
        return mapped;
    }

    public List<GroupChatDto> GetGroupChats(BaseRequest request, Claim userIdClaim)
    {
        try
        {
            var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user not found");
            var usrAccount = _repUserAccount.GetUserAccount(userId) ?? throw new Exception("user not found");

            var usrGroups = _repGroupChat.GetGroupChatsIdsByUserId(usrAccount.UserId);
            var data = _repGroupChat.GetGroupChats()
                .Where(grpc => usrGroups.Contains(userId))
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();
            
            var mapped = _mapper.Map<List<GroupChatDto>>(data);

            foreach (var grpcMapped in mapped)
            {
                grpcMapped.Members = _repGroupChat.GetGroupChatMembersIds(grpcMapped.GroupChatId);
            }
            
            return mapped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GroupChatDto> UpdateGroupChat(GroupChatUpdateRequest request, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, request.GroupChatId, userIdClaim);
            var grpChat = verify.GroupChat;
            
            if (request.Name is not null && grpChat.Name != request.Name && _repGroupChat.GetGroupChat(request.Name) is not null) throw new Exception("name taked");
            if (request.InviteCode is not null && grpChat.InviteCode != request.InviteCode && _repGroupChat.GetGroupChatByInviteCode(request.InviteCode) is not null) throw new Exception("invite code taked");
            
            grpChat.Name = request.Name ?? grpChat.Name;
            grpChat.InviteCode = request.InviteCode ?? grpChat.InviteCode;
            grpChat.Private = request.Private ?? grpChat.Private;
            grpChat.AvatarUrl = request.AvatarUrl ?? grpChat.AvatarUrl;
            var updGroupChat = await _repGroupChat.UpdateGroupChat(grpChat);
            
            var mapped = _mapper.Map<GroupChatDto>(updGroupChat);
            mapped.Members = _repGroupChat.GetGroupChatMembersIds(updGroupChat.GroupChatId);
            
            return mapped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> JoinToGroupChat(GroupChatJoinRequest request, Claim userIdClaim)
    {
        try
        {
            var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user not found");
            var usrAccount = _repUserAccount.GetUserAccount(userId) ?? throw new Exception("user not found");

            var grpChat = _repGroupChat.GetGroupChatByInviteCode(request.InviteCode) ?? throw new Exception("invite code invalid");
            
            var grpChatMember = _repGroupChat.GetGroupChatMemberByUserId(grpChat.GroupChatId, usrAccount.UserId);
            if (grpChatMember is not null) throw new Exception("user already belongs to the group");

            if (grpChat.Private && string.IsNullOrWhiteSpace(request.Password)) throw new Exception("when a group chat is private, password is required");
            if (request.Password is not null && grpChat.Password is not null && !Hasher.Compare(request.Password, grpChat.Password)) throw new Exception("password for group chat is invalid");
            
            await _repGroupChat.CreateGroupChatMember(new Groupchatmember()
            {
                GroupChatId = grpChat.GroupChatId,
                UserId = usrAccount.UserId,
            });

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> LeaveToGroupChat(Guid groupChatId, Claim userIdClaim)
    {
        try
        {
            var userId = Parser.Guid(userIdClaim.Value) ?? throw new Exception("user not found");
            var usrAccount = _repUserAccount.GetUserAccount(userId) ?? throw new Exception("user not found");

            var grpChat = _repGroupChat.GetGroupChat(groupChatId) ?? throw new Exception("group chat not found");
            
            var grpChatMember = _repGroupChat.GetGroupChatMemberByUserId(grpChat.GroupChatId, usrAccount.UserId);
            if (grpChatMember is null) throw new Exception("user does not belong to the chat group");
            
            if (grpChatMember.UserId == grpChat.OwnerId) throw new Exception("user is already owner of group. Transfer ownership to other member");

            var result = await _repGroupChat.DeleteGroupChatMember(grpChatMember);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GroupChatDto> TransferOwnershipGroupChat(GroupChatTransferOwnershipRequest request, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, request.GroupChatId, userIdClaim, Consts.RoleGroupChatMemberOwner);
            var grpChat = verify.GroupChat;

            var currentOwner = verify.GroupChatMember;
            var newOwner = _repGroupChat.GetGroupChatMemberByUserId(grpChat.GroupChatId, request.NewOwnerId) ?? throw new Exception("the new owner, must be part of the chat group");
            
            // Update members
            currentOwner.Role = Consts.RoleGroupChatMemberCommon;
            newOwner.Role = Consts.RoleGroupChatMemberOwner;
            
            var updOwnership = await _repGroupChat.TransferGroupChatOwnership(grpChat, currentOwner, newOwner);
            if (!updOwnership) throw new Exception("the transfer ownership failed");
            
            var mapped = _mapper.Map<GroupChatDto>(grpChat);
            mapped.Members = _repGroupChat.GetGroupChatMembersIds(grpChat.GroupChatId);

            return mapped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GroupChatDto> ChangePasswordGroupChat(GroupChatChangePasswordRequest request, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, request.GroupChatId, userIdClaim);
            var grpChat = verify.GroupChat;
            
            if (!grpChat.Private) throw new Exception("group chat does not have a private state");
            if (grpChat.Password is not null && !Hasher.Compare(request.CurrentPassword, grpChat.Password)) throw new Exception("password current is not the same as the old password");
            if (request.NewPassword.Equals(request.ConfirmNewPassword)) throw new Exception("passwords do not match");
            
            grpChat.Password = Hasher.Hash(request.NewPassword);
            var updGroupChat = await _repGroupChat.UpdateGroupChat(grpChat);
            
            var mapped = _mapper.Map<GroupChatDto>(updGroupChat);
            return mapped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteGroupChat(Guid groupChatId, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, groupChatId, userIdClaim);
            var grpChat = verify.GroupChat;
            
            return await _repGroupChat.DeleteGroupChat(grpChat);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Administrator
    public async Task<bool> UpdateGroupChatMember(GroupChatUpdateMemberRequest request, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, request.GroupChatId, userIdClaim);
            var grpChat = verify.GroupChat;
            var grpChatAdministrator = verify.GroupChatMember;

            var grpChatOwner = _repGroupChat.GetGroupChatOwner(grpChat.GroupChatId) ?? throw new Exception("group chat not found");
            if (request.MemberId == grpChatOwner.MemberId) throw new Exception("an administrator cannot update the group chat owner");
            
            if (request.MemberId == grpChatAdministrator.MemberId) throw new Exception("member id is equal to administrator id");
            
            var grpChatMember = _repGroupChat.GetGroupChatMemberByMemberId(grpChat.GroupChatId, request.MemberId);
            if (grpChatMember is null) throw new Exception("user does not belong to the chat group");

            if (request.MemberRole is not null && request.MemberRole > grpChatAdministrator.Role && grpChatMember.RoleGrantedBy is not null &&  grpChatMember.RoleGrantedBy != grpChatAdministrator.MemberId) throw new Exception("an administrator cannot change the role of another administrator, unless he has changed it himself"); // <- role granted by some administrator
            
            if (request.MemberRole is not null && request.MemberRole < grpChatAdministrator.Role) throw new Exception("member role is less than administrator role");
            
            grpChatMember.Role = request.MemberRole ?? grpChatMember.Role;
            grpChatMember.RoleGrantedBy = request.MemberRole is not null ? grpChatAdministrator.MemberId : null;
            grpChatMember.AvatarUrl = request.MemberAvatarUrl ?? grpChatMember.AvatarUrl;
            await _repGroupChat.UpdateGroupChatMember(grpChatMember);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteGroupChatMember(GroupChatDeleteMemberRequest request, Claim userIdClaim)
    {
        try
        {
            var verify = Verify.MemberPermissions(_repGroupChat, request.GroupChatId, userIdClaim);
            var grpChat = verify.GroupChat;
            var grpChatAdministrator = verify.GroupChatMember;
            
            var grpChatOwner = _repGroupChat.GetGroupChatOwner(grpChat.GroupChatId) ?? throw new Exception("group chat not found");
            if (request.MemberId == grpChatOwner.MemberId) throw new Exception("an administrator cannot update the group chat owner");
            
            if (request.MemberId == grpChatAdministrator.MemberId) throw new Exception("member id is equal to administrator id");
            
            var grpChatMember = _repGroupChat.GetGroupChatMemberByMemberId(grpChat.GroupChatId, request.MemberId);
            if (grpChatMember is null) throw new Exception("user does not belong to the chat group");
            
            if (grpChatAdministrator.Role == grpChatMember.Role) throw new Exception("member role is equal to administrator role");

            var delGroupChatMember = await _repGroupChat.DeleteGroupChatMember(grpChatMember);
            return delGroupChatMember;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}