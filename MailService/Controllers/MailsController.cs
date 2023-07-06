using MailKit.Net.Smtp;
using MailService.Configuration;
using MailService.Data;
using MailService.Models.APIIn;
using MailService.Models.APIOut;
using MailService.Models.Common;
using MailService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly MailContext mailContext;
        private readonly ILogger<MailsController> logger;
        private readonly IMailingService mailingService;
        public MailsController(MailContext mailContext, ILogger<MailsController> logger, IMailingService mailingService)
        {
            this.mailContext = mailContext;
            this.logger = logger;
            this.mailingService = mailingService;
        }
        /// <summary>
        /// Возвращает список всех отправленных e-mail вместе с успешностью отправки и сообщениями об ошибках
        /// </summary>
        /// <returns>JSON-массив всех отправленных сообщений</returns>
        [HttpGet]
        public JsonResult Get() {
            var mails = mailContext.Mails.Select(x => new Models.APIOut.Mail
            {
                Body = x.Body,
                Subject = x.Subject,
                FailedReason = x.FailedReason,
                Recipients = x.Recipients.Select(y => y.Name).ToList(),
                Status = x.Status
            });
            return new JsonResult(mails);
        }
        /// <summary>
        /// Отправляет электронное письмо с использованием заданных в файле конфигурации данных аутентификации
        /// </summary>
        /// <param name="mail">Содержимое и получатели электронного письма</param>
        /// <returns>Результат обработки запроса</returns>
        [HttpPost]
        public IActionResult Post(Models.APIIn.Mail mail) {

            
            var result = mailingService.SendMail(mail);

            var mailDB = new Models.DB.Mail() { 
                Status = result.Status,
                FailedReason = result.Responce,
                Body = mail.Body, 
                Subject = mail.Subject, 
                Recipients = mail.Recipients.Select(x => new Models.DB.Recipient() { Name = x }).ToList() 
            };
            mailContext.Mails.Add(mailDB);
            mailContext.SaveChanges();
            if(result.Status == MailStatus.Ok)
                return Ok();
            else
                return BadRequest(result.Responce);
        }
    }
}
