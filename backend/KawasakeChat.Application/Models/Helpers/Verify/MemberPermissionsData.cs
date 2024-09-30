using KawasakeChat.Domain.Entities;

namespace KawasakeChat.Application.Models.Helpers.Verify;

public class MemberPermissionsData
{
    public Groupchat GroupChat { get; set; } = null!;
    public Groupchatmember GroupChatMember { get; set; } = null!;
}