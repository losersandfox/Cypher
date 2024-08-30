
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
           : base(options)
        { }

        DbSet<User> Users { get; set; }
        DbSet<PhyHost> PhyHosts { get; set; }
        DbSet<VirtualHost> VirtualHosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<PhyHost>().ToTable("PhyHost");
            builder.Entity<VirtualHost>().ToTable("VirtaulHost");

            builder.Entity<User>()
                .HasMany<PhyHost>(u => u.LinksHosts)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId)
                .IsRequired();

            builder.Entity<PhyHost>()
                .HasMany<VirtualHost>(h => h.VirtualHosts)
                .WithOne(v => v.Host)
                .HasForeignKey(v => v.HostId)
                .IsRequired();
        }
    }
}
