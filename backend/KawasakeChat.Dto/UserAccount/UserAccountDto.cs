namespace KawasakeChat.Dto.UserAccount;

public class UserAccountDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Status { get; set; }
    public int Role { get; set; }
    public DateTime CreatedAt { get; set; }
}