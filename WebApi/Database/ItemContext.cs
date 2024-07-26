
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany<PhyHost>(u => u.LinksHosts)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId)
                .IsRequired();

            builder.Entity<User>()
                .HasOne(u => u.NowHost)
                .WithOne(h => h.User)
                .HasForeignKey<PhyHost>(h => h.UserId)
                .IsRequired();

            builder.Entity<PhyHost>()
                .HasMany<VirtualHost>(h => h.VirtualHosts)
                .WithOne(v => v.Host)
                .HasForeignKey(v => v.HostId)
                .IsRequired();

            builder.Entity<PhyHost>()
                .HasOne(h => h.SysData)
                .WithOne(s => s.Host)
                .HasForeignKey<PhyHost>(s => s.HostId)
                .IsRequired();

            builder.Entity<VirtualHost>()
                .HasOne(v => v.VirtualSysData)
                .WithOne(s => s.VirtualHost)
                .HasForeignKey<VirtualHost>(s => s.VirtualHostId)
                .IsRequired();

            builder.Entity<SysData>()
                .HasMany(s => s.processes)
                .WithOne(p => p.SysData)
                .HasForeignKey(p => p.SysDataId)
                .IsRequired();

        }
    }
}
