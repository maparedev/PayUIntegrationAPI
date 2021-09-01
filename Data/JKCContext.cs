using JckShopping.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JckShopping.Data
{
    public class JKCContext : IdentityDbContext<JKCStoreUser>
    {       

        public JKCContext(DbContextOptions<JKCContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<PaymentGateways> PaymentGateways { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{            
        //    base.OnConfiguring(optionsBuilder);        
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Order>()
        //        .HasData(new Order()
        //        {
        //            Id = 1,
        //            OrderDate = DateTime.UtcNow,
        //            OrderNumber = "1234"
        //        });

        //}
    }
}
