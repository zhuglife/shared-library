namespace Security.Common.Models;

public class UserClaims
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public Dictionary<string, string> CustomClaims { get; set; } = new();
}
