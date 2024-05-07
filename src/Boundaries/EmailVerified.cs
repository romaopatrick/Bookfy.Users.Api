using Bookfy.Users.Api.Boundaries;
using MassTransit;

namespace Bookfy.Users.Api.Boundaries
{
    public class EmailVerified : CorrelatedByGuid
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
    }
}