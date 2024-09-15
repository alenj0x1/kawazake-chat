namespace KawasakeChat.Dto.UserAccount;

public class UserAccountCreateDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Status { get; set; }
    public int Role { get; set; }
}