using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Contexts
{
    public class PersonalAccountContext : DbContext
    {
        public PersonalAccountContext(DbContextOptions<PersonalAccountContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PersonalAccount> PersonalAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\Database\\personalAccount.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalAccount>()
                .HasIndex(x => x.NumberPA).IsUnique();
        }
    }
}