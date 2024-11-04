using System;

namespace lesson_261024.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
