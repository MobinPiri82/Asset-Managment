using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Proxies;

namespace NutShell
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons {  get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {   
            options
            .UseLazyLoadingProxies()
            .UseSqlServer("Data Source=.\\MOBIN;\r\nDatabase=Asset Managmet;\r\nIntegrated Security=True;\r\nTrustServerCertificate=True\r\n");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Asset>()
            //    .HasOne(a => a.Owner)
            //    .WithMany(a => a.Assets)
            //    .HasForeignKey(a => a.Owner);

            modelBuilder.Entity<Person>()
                .HasMany(a => a.Assets)
                .WithOne(p => p.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
// eager loading
// lazy loading