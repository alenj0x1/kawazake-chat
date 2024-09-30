using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatCreateRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(255)]
    public string? AvatarUrl { get; set; }
    public bool Private { get; set; } = false;
    [MaxLength(255)]
    public string? Password { get; set; }
}