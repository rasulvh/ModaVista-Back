using MailKit.Security;
using MimeKit;
using ModaVista_Back.Services.Interfaces;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace ModaVista_Back.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? "rasulvh@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("rasulvh@code.edu.az", "isxmeulkiwgdbejg");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
