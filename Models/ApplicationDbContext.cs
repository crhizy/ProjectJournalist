using Microsoft.EntityFrameworkCore;
using ProjectJournalist.ViewModels;

namespace ProjectJournalist.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // DbSet para cada entidad en tu base de datos
        public DbSet<User> Users { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }


    }
}

