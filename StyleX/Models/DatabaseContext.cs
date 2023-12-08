using Microsoft.EntityFrameworkCore;
using StyleX.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StyleX.Models
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; } = null!;


        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
    : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Admin
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
        }
    }
}