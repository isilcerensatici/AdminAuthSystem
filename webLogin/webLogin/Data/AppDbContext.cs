using Microsoft.EntityFrameworkCore;
using webLogin.Models;

namespace webLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasKey(u => u.Username);
        }
    }

}
