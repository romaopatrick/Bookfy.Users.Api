using System.Net;
using System.Net.Mail;
using System.Text;
using Bookfy.Users.Api.src.Ports;
using Microsoft.Extensions.Options;

namespace Bookfy.Users.Api.Adapters;
public class EmailSender(
    IOptions<EmailSenderSettings> opts
    ) : IEmailSender
{
    private readonly EmailSenderSettings _settings = opts.Value;
    public void Send(
        string? from, string to,  string subject,
        string body, string[]? cc = default, params Attachment[] attachments)
    {
        var mail = new MailMessage(from ?? _settings.Username, to, subject, body)
        {
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
        };

        foreach (var c in cc ?? [])
            mail.CC.Add(c);

        foreach (var attachment in attachments)
            mail.Attachments.Add(attachment);

        using var client = new SmtpClient(
            _settings.Server,
            _settings.Port)
        {
            Credentials = new NetworkCredential(
                _settings.Username, 
                _settings.Password),
            EnableSsl = _settings.EnableSsl
        };

        client.Send(mail);
    }
}