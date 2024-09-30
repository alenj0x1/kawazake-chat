using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatChangePasswordRequest
{
    [Required] 
    public Guid GroupChatId { get; set; }
    
    [Required] 
    [MaxLength(255)] 
    public string CurrentPassword { get; set; } = null!;

    [Required] 
    [MaxLength(255)] 
    public string NewPassword { get; set; } = null!;

    [Required] 
    [MaxLength(255)] 
    public string ConfirmNewPassword { get; set; } = null!;
}