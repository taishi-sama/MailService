using MailKit.Net.Smtp;
using MailService.Configuration;
using MailService.Controllers;
using MailService.Models.APIIn;
using MailService.Models.Common;
using MimeKit;
using System.Diagnostics;

namespace MailService.Services
{
    public class MailingService : IMailingService
    {
        private readonly ILogger<MailingService> logger;
        private readonly IConfiguration configuration;
        public MailingService(ILogger<MailingService> logger, IConfiguration configuration) 
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public MailingResult SendMail(Mail mail)
        {
            string? responce = null;
            MailStatus status = MailStatus.Ok;

            using (var client = new SmtpClient())
            {
                try
                {
                    var mailConfig = configuration.GetSection(SMTPOptions.SMTP).Get<SMTPOptions>();
                    if (mailConfig != null)
                    {
                        var message = new MimeMessage();
                        message.Subject = mail.Subject;
                        foreach (var x in mail.Recipients)
                        {
                            message.To.Add(new MailboxAddress("", x));
                        }
                        message.Body = new TextPart("plain") { Text = mail.Body };
                        message.Sender = new MailboxAddress("", mailConfig.Sender);
                        client.Connect(mailConfig.Host, int.Parse(mailConfig.Port), true);
                        logger.LogInformation("Connected to {}:{}", mailConfig.Host, mailConfig.Port);
                        client.Authenticate(mailConfig.Login, mailConfig.Password);

                        var send_responce = client.Send(message);
                        if (!send_responce.Contains("Ok", StringComparison.OrdinalIgnoreCase))
                        {
                            status = MailStatus.Failed;
                            responce = send_responce;
                        }
                        client.Disconnect(true);
                    }
                    else
                    {
                        logger.LogCritical("Correct configuration for SMTP not found!");
                        status = MailStatus.Failed;
                        responce = "Config not found";
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Error while sending message!");
                    status = MailStatus.Failed;
                    responce = ex.Message;
                }
            }
            return new MailingResult() { Status = status, Responce = responce };
        }
    }
}
