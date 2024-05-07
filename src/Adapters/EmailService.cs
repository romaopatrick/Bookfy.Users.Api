using Bookfy.Users.Api.Boundaries;
using Bookfy.Users.Api.Ports;
using Bookfy.Users.Api.src.Ports;
using Microsoft.Extensions.Options;

namespace Bookfy.Users.Api.Adapters
{
    public class EmailService(
        IEmailSender sender,
        IOptions<EmailServiceSettings> opts) : IEmailUseCase
    {
        private readonly IEmailSender _sender = sender;
        private readonly EmailServiceSettings _settings = opts.Value;

 // You can use this method to generate a token for email verification
        
        public Result<VerifyEmailSent> SendVerifyEmail(SendVerifyEmail input, CancellationToken ct)
        {
            _sender.Send(
                null,
                input.Email,
                "Verificação de e-mail",
                ReadHtmlFile("resources/verify-email.html")
                    .Replace("{{Name}}", input.Name)
                    .Replace("{{Link}}", input.BuildLink(_settings.PasswordCreationHostAddress)));


            return Result.WithSuccess(new VerifyEmailSent
            {
                Email = input.Email,
                Name = input.Name,
            }, 200);
        }

        private static string ReadHtmlFile(string path)
        {
            var file = File.ReadAllText(path);

            return file;
        }

    }
}