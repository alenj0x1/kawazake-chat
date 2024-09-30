using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatUpdateRequest
{
    [Required]
    public Guid GroupChatId { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }
    [MaxLength(255)]
    public string? AvatarUrl { get; set; }
    [MaxLength(50)]
    public string? InviteCode { get; set; }
    [MaxLength(255)]
    public string? Password { get; set; }
    public bool? Private { get; set; }
}