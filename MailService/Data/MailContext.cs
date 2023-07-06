using MailService.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace MailService.Data
{
    public class MailContext : DbContext
    {
        public MailContext(DbContextOptions<MailContext> options) : base(options)
        {
        }

        public DbSet<Mail> Mails {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlite());
        }
    }
}
