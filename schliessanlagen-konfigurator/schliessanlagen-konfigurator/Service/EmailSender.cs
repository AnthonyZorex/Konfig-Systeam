using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace schliessanlagen_konfigurator.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;

        public EmailSender(IConfiguration configuration)
        {
            // Получаем настройки SMTP из конфигурации
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUsername = configuration["EmailSettings:SmtpUsername"];
            _smtpPassword = configuration["EmailSettings:SmtpPassword"];
            _fromEmail = configuration["EmailSettings:FromEmail"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(email);

            // Отправляем письмо асинхронно
            await client.SendMailAsync(message);
        }
    }
}
