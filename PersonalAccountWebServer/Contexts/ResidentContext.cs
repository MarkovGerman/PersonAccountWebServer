using Microsoft.EntityFrameworkCore;
using PersonalAccountWebServer.Models;

namespace PersonalAccountWebServer.Contexts
{
    public class ResidentContext : DbContext
    {
        public ResidentContext(DbContextOptions<ResidentContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Resident> Residents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\Database\\resident.db");
        }
    }
}