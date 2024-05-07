using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Bookfy.Users.Api.src.Adapters
{
    public static class StringExtensions
    {
        public static string Hash(this string value)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes($"{value}.{GenerateSalt()}"));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private static string GenerateSalt() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        public static bool ValidEmail(this string value)
        {
            var valid = true;
            try
            {
                _ = new MailAddress(value);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}