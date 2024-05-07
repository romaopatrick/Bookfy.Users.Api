using Bookfy.Users.Api.Boundaries;

namespace Bookfy.Users.Api.Boundaries;

public class CreateReader : CorrelatedByGuid
{
    public required string Username { get; set; }
    public string? Name { get; set; }
    public required string Email { get; set; }
}