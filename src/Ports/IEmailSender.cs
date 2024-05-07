using System.Net.Mail;

namespace Bookfy.Users.Api.src.Ports
{
    public interface IEmailSender
    {
        void Send(
            string? from, string to, string subject,
            string body, string[] cc = default!, params Attachment[] attachments);
    }
}