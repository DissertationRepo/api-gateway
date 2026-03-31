namespace api_gateway.Api.Models.IdentityService
{
    public class LoginRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
        public string? RefreshToken { get; init; }
    }
}
