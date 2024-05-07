using System.Net;
using Bookfy.Users.Api.Boundaries;
using Bookfy.Users.Api.Ports;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Bookfy.Users.Api.Adapters;
public static class UserRoutes
{
    public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder app)
    {
        var usersGroup = app.MapGroup("/users");

        usersGroup.MapPost("", async (
            CancellationToken ct,
            IUserUseCase useCase,
            IBus bus,
            [FromBody] CreateUser input
        ) =>
        {
            var result = await useCase.Create(input, ct);

            if(result.Success)
                await bus.Publish(result.Data!, ct);
                
            return JsonFromResult(result);
        });

        return app;
    }

    private static IResult JsonFromResult<T>(Result<T> result) =>
           result.Code == (int)HttpStatusCode.NoContent
               ? Results.NoContent()
               : Results.Json(
                   data: result,
                   statusCode: result.Code);
}