using MailService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailService
{
    public class MailContextFactory : IDesignTimeDbContextFactory<MailContext>
    {
        /// <summary>
        /// Создаёт контекст базы данных для работы средств миграции
        /// </summary>
        public MailContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MailContext>();
            optionsBuilder.UseSqlite("Data Source=./DB/mail.sqlite");

            return new MailContext(optionsBuilder.Options);
        }
    }
}
