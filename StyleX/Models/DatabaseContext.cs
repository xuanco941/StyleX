using Microsoft.EntityFrameworkCore;
using StyleX.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace StyleX.Models
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ProductDesign> ProductDesigns { get; set; } = null!;
        public DbSet<DesignInfo> DesignInfos { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;






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
            //Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.keyActive).IsUnique();
            });
            //Admin
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }

    }
}