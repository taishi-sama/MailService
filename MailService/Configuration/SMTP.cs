namespace MailService.Configuration
{
    public class SMTPOptions
    {
        public const string SMTP = "SMTP";
        /// <summary>
        /// Адрес SMTP-сервера
        /// </summary>
        public required string Host { get; set; }
        /// <summary>
        /// SSL-порт SMTP-сервера
        /// </summary>
        public required string Port { get; set; }
        /// <summary>
        /// Логин для авторизации на SMTP-сервере
        /// </summary>
        public required string Login { get; set; }
        /// <summary>
        /// Пароль для авторизации на SMTP-сервере
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// E-mail, указываемый как отправитель электронного письма
        /// </summary>
        public required string Sender { get; set; }
        
    }
}
