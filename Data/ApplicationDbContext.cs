using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LibApp.Models;
using Microsoft.AspNetCore.Identity;

namespace LibApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .ToTable("Customers", "dbo").Property(p => p.Id).HasColumnName("CustomerId");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("CustomerRoles");
        }
    }
}