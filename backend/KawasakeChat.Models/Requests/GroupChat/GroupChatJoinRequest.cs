using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatJoinRequest
{
    [Required]
    [MaxLength(50)]
    public string InviteCode { get; set; } = null!;
    public string? Password { get; set; }
}