namespace api_gateway.Api.Models.IdentityService
{
    public class RefreshRequest
    {
        public required string RefreshToken { get; init; }
        public required string UserId { get; init; }
    }
}
