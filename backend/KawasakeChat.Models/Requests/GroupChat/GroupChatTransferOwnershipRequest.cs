using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.GroupChat;

public class GroupChatTransferOwnershipRequest
{
    [Required]
    public Guid NewOwnerId { get; set; }
    [Required]
    public Guid GroupChatId { get; set; }
}