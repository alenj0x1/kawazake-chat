using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests.Auth;

public class LoginRequest
{
    [Required]
    [MaxLength(32)]
    public string Username { get; set; } = null!;
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = null!;
}