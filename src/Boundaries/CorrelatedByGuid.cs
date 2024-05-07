using MassTransit;

namespace Bookfy.Users.Api.Boundaries
{
    public class CorrelatedByGuid : CorrelatedBy<Guid>
    {
        public required Guid CorrelationId { get; set; }

    }
}