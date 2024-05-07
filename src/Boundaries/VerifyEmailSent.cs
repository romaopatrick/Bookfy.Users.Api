namespace Bookfy.Users.Api.Boundaries
{
    public class VerifyEmailSent
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
    }
}