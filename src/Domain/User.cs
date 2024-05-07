namespace Bookfy.Users.Api.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public string? VerifyEmailToken { get; set; }
        public bool EmailVerified { get; set; }
        public string? Password { get; set; }
    }
}