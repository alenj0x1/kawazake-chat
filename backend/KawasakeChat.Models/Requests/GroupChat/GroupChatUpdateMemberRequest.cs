using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatUpdateMemberRequest
{
    [Required]
    public Guid GroupChatId { get; set; }
    [Required]
    public Guid MemberId { get; set; }
    public int? MemberRole { get; set; }
    public string? MemberAvatarUrl { get; set; }
}