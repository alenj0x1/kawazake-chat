﻿namespace KawasakeChat.Models.Responses.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}