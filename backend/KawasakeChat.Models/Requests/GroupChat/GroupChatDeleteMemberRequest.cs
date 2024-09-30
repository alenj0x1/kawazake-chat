using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatDeleteMemberRequest
{
    [Required]
    public Guid GroupChatId { get; set; }
    [Required]
    public Guid MemberId { get; set; }
}