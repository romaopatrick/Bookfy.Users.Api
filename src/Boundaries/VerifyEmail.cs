namespace Bookfy.Users.Api.Boundaries
{
    public class VerifyEmail
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string EmailToken { get; set; }
    }
}