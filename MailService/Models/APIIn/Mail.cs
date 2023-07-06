using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailService.Models.APIIn
{
    /// <summary>
    /// Содержимое входящего запроса на отправку e-mail 
    /// </summary>
    public class Mail : IValidatableObject
    {
        /// <summary>
        /// Тема сообщения
        /// </summary>
        [JsonRequired]
        [Required]
        public required string Subject { get; init; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonRequired]
        [Required]
        public required string Body { get; init; }
        /// <summary>
        /// E-mail адреса получателей
        /// </summary>
        [JsonRequired]
        [Required]
        public required List<string> Recipients { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Recipients.Count == 0)
                yield return new ValidationResult("List of recipients must contain at least one email", new[] { nameof(Recipients) });
            foreach (var item in Recipients)
            {
                int index = item.IndexOf('@');
                if (index > 0 &&
                    index != item.Length - 1 &&
                    index == item.LastIndexOf('@'))
                    yield return ValidationResult.Success;
                else
                    yield return new ValidationResult("List of recipients must contain only valid emails", new[] { nameof(Recipients) });

            }
        }
    }
}
