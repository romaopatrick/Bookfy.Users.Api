namespace Bookfy.Users.Api.Adapters;
public class EmailSenderSettings
{
    public required string Server { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required int Port { get; init; }
    public required bool EnableSsl { get; init; }
}