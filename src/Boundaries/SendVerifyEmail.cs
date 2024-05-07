using MassTransit;

namespace Bookfy.Users.Api.Boundaries
{
    public class SendVerifyEmail : CorrelatedByGuid
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string VerifyEmailToken { get; set; }
        public string BuildLink(string hostAddress) 
            => $"{hostAddress}/verify-email/{VerifyEmailToken}";
    }
}