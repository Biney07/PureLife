using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.ViewModel.Email;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailViewModel email) {
            try
            {
                // Command-line argument must be the SMTP host.
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;


                // Set your credentials for the SMTP server (e.g., Gmail).
                client.Credentials = new NetworkCredential("purelifeabr@gmail.com", "qcdaoiwlwakdngvm");

                // Specify the email sender.
                MailAddress from = new MailAddress("purelifeabr@gmail.com", "PURELIFE");

                // Set the recipient's email address.
                MailAddress to = new MailAddress(email.RecipentEmail);

                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Subject = email.Subject;
                message.Body = email.Body;

                await client.SendMailAsync(message);

                // Clean up.
                message.Dispose();
                client.Dispose();

                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
    }
}

