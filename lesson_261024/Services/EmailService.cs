using System;
using System.Net;
using System.Net.Mail;
using lesson_261024.Services.Interfaces;

namespace lesson_261024.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("viosagmir@gmail.com","application_password"),
            EnableSsl = true
        };

        var mailMessage = new MailMessage("viosagmir@gmail.com", to, subject, body);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
