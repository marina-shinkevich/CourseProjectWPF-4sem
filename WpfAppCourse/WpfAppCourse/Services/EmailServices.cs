using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace WpfAppCourse.Services
{
    public class EmailService
    {
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly bool _enableSsl;

        public EmailService()
        {
            _senderEmail = ConfigurationManager.AppSettings["EmailSender"] ??
                throw new ArgumentNullException("EmailSender не настроен");
            _senderPassword = ConfigurationManager.AppSettings["EmailPassword"] ??
                throw new ArgumentNullException("EmailPassword не настроен");
            _smtpHost = ConfigurationManager.AppSettings["SmtpHost"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
            _enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"] ?? "true");
        }

        public async Task SendAppointmentConfirmationAsync(
            string recipientEmail,
            string recipientName,
            string serviceName,
            DateTime appointmentDate,
            TimeSpan appointmentTime,
            string masterName)
        {
            if (string.IsNullOrEmpty(recipientEmail))
                throw new ArgumentNullException(nameof(recipientEmail), "Email пользователя не указан");

            var fromAddress = new MailAddress(_senderEmail, "Косметологический центр");
            var toAddress = new MailAddress(recipientEmail);

            string subject = "Подтверждение записи";
            string body = $"Здравствуйте, {recipientName}!\n\n" +
                         $"Вы записаны на процедуру '{serviceName}' " +
                         $"на {appointmentDate:dd.MM.yyyy} в {appointmentTime:hh\\:mm}.\n\n" +
                         $"Мастер: {masterName}";

            using (var smtp = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = _enableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _senderPassword),
                Timeout = 5000
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                })
                {
                    try
                    {
                        await smtp.SendMailAsync(message).ConfigureAwait(false);
                    }
                    catch (SmtpException smtpEx) when (smtpEx.StatusCode == SmtpStatusCode.MailboxUnavailable)
                    {
                        throw new Exception("Указанный почтовый адрес не существует или недоступен", smtpEx);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ошибка при отправке письма", ex);
                    }
                }
            }
        }
    }

}

