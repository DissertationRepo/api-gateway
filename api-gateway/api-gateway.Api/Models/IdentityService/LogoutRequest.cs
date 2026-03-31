namespace api_gateway.Api.Models.IdentityService
{
    public class LogoutRequest
    {
        public required string RefreshToken { get; init; }
    }
}
