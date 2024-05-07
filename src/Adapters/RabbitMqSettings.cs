using MassTransit;

namespace Bookfy.Users.Api.Adapters
{
    public class RabbitMqSettings
    {
        public required string Host { get; set; }
        public string? VirtualHost { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}