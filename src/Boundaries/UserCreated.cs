using MassTransit;

namespace Bookfy.Users.Api.Boundaries
{
    public class UserCreated : CorrelatedBy<Guid>
    {
        public Guid Id { get; set; }
        public required string Email { get; init; }
        public required string Name { get; init; }
        public required string VerifyEmailToken { get; init; }

        public Guid CorrelationId { get; set; }

    }
}