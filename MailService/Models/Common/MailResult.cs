using System.Text.Json.Serialization;

namespace MailService.Models.Common
{
    /// <summary>
    /// Статус успешности отправки сообщения
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MailStatus
    {
        Ok,
        Failed
    }
}
