namespace KawasakeChat.Dto.GroupChat;

public class GroupChatDto
{
    public Guid GroupChatId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public string InviteCode { get; set; } = null!;
    public bool Private { get; set; }
    public List<Guid> Members { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}