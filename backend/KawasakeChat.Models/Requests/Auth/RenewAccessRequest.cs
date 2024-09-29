namespace KawasakeChat.Models.Requests.Auth;

public class RenewAccessRequest
{
    public string RefreshToken { get; set; } = null!;
}