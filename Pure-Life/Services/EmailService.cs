using Pure_Life.ViewModel.Email;
using System.Net;
using System.Net.Mail;

namespace Pure_Life.Services
{
    public class EmailService
    {
        private readonly SmtpClient _client;
        public EmailService(SmtpClient client)
        {
            _client = new SmtpClient("smtp.gmail.com");
            _client.EnableSsl = true;
            _client.Credentials = new NetworkCredential("purelifeabr@gmail.com", "qcdaoiwlwakdngvm");
        }
        public async Task<string> SendEmailAsync(EmailViewModel email)
        {
            try
            {
                MailAddress from = new MailAddress("purelifeabr@gmail.com", "PURELIFE");

                // Set the recipient's email address.
                MailAddress to = new MailAddress(email.RecipentEmail);

                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Subject = email.Subject;
                message.Body = email.Body;

                await _client.SendMailAsync(message);

                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                return $"Failed to send email: {ex.Message}";
            }
        }
    }
}

