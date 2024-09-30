using System.ComponentModel.DataAnnotations;

namespace KawasakeChat.Models.Requests;

public class UserAccountCreateRequest
{
    [Required]
    [MaxLength(32)]
    public string Username { get; set; } = null!;
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = null!;
    [MaxLength(255)]
    public string? AvatarUrl { get; set; }
    [MaxLength(255)]
    public string? Status { get; set; }
}