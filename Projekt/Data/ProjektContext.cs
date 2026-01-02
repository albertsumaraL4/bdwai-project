using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ProjektContext : DbContext
    {
        public ProjektContext(DbContextOptions<ProjektContext> options)
            : base(options)
        {
        }


        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<User> Users { get; set; }
    }
}