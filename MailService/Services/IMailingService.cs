using MailService.Models.APIIn;
using MailService.Models.Common;

namespace MailService.Services
{

    public interface IMailingService
    {
        /// <summary>
        /// Отправляет электронное письмо с использованием заданных в файле конфигурации данных аутентификации
        /// </summary>
        /// <param name="mail">Содержимое и получатели электронного письма</param>
        /// <returns>Результат успешности отправки и сообщение об ошибке(если необходимо)</returns>
        public MailingResult SendMail(Mail mail);
    }
    public class MailingResult
    {
        /// <summary>
        /// Сообщение об ошибке в случае неуспешной отправки
        /// </summary>
        public string? Responce { get; init; }
        /// <summary>
        /// Статус успешности отправки сообщения
        /// </summary>
        public required MailStatus Status { get; init; }
    }
}
