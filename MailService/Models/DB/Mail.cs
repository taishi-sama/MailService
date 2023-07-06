using MailService.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MailService.Models.DB
{
    
    public class Mail
    {
        public long Id { get; set; }
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
        public string Subject { set; get; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Body { set; get; }
        /// <summary>
        /// E-mail адреса получателей
        /// </summary>
        public List<Recipient> Recipients { set; get; }

    }
    public class Recipient
    {
        public long Id { get; set; }
        [ForeignKey(nameof(Mail))]
        public long MailKey { get; set; }
        public Mail Mail { set; get; }
        /// <summary>
        /// E-mail адрес получателя
        /// </summary>
        public string Name { set; get; }

    }
}
