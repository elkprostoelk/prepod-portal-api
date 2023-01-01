using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrepodPortal.Common.Configurations;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.Core.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailConfiguration _configuration;

    public EmailService(
        ILogger<EmailService> logger,
        IOptions<EmailConfiguration> emailConfiguration)
    {
        _logger = logger;
        _configuration = emailConfiguration.Value;
    }

    public async Task SendEmailAsync(string name, string email, string password)
    {
        using var smtpClient = new SmtpClient
        {
            Host = _configuration.Host,
            Port = _configuration.Port,
            UseDefaultCredentials = _configuration.UseDefaultCredentials,
            EnableSsl = _configuration.EnableSsl,
            Credentials = new System.Net.NetworkCredential(
                _configuration.UserName, _configuration.Password),
            DeliveryMethod = _configuration.DeliveryMethod
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_configuration.UserName),
            Subject = $"Ласкаво просимо на еНауковаДекларація, {name}",
            Body = @$"
                    Вітаємо, {name}.
                    Вас зареєстрував на еНауковаДекларація працівник кафедри. Ваші дані для входу:
                    1) Ім'я користувача: {email}
                    2) Пароль: {password}
                    http://tutor-net.kspu.edu
                    "
        };
        mail.To.Add(new MailAddress(email));
        
        try
        {
            await smtpClient.SendMailAsync(mail);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.InnerException ?? e, "An exception occured when executing the service");
        }
    }
}