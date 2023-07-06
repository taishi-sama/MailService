using MailService.Models.Common;
using MailService.Models.DB;
using System.Text.Json.Serialization;

namespace MailService.Models.APIOut
{
    /// <summary>
    /// Содержимое исходящего запроса списка отправленных сообщений 
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Статус успешности отправки сообщения
        /// </summary>
        public MailStatus Status { set; get; }
        /// <summary>
        /// Сообщение при неудачной отправке
        /// </summary>
        public string? FailedReason { set; get; }
        /// <summary>
        /// Тема сообщения
        /// </summary>
        public required string Subject { set; get; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public required string Body { set; get; }
        /// <summary>
        /// E-mail адреса получателей
        /// </summary>
        public required List<string> Recipients { set; get; }
    }
}
