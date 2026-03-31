namespace api_gateway.Api.Models.IdentityService
{
    public class RegisterRequest
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required string Role { get; init; }
    }
}
