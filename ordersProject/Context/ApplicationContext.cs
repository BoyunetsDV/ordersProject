using Microsoft.EntityFrameworkCore;
using ordersProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<OrderArticle> OrderArticles { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OrderProcessingSystem;Trusted_Connection=True;");
        }
    }
}
