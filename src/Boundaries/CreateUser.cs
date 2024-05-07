using System.ComponentModel.DataAnnotations;
using Bookfy.Users.Api.src.Adapters;

namespace Bookfy.Users.Api.Boundaries;

public class CreateUser
{
    public required string Email { get; init; }
    public required string Name { get; init; }

    public Result Validate()
    {
        if (!Email.ValidEmail())
            return Result.WithFailure("invalid_email", 400);

        return Result.WithSuccess(200);
    }
}