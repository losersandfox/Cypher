﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ItemContext))]
    [Migration("20240821160658_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entity.PhyHost", b =>
                {
                    b.Property<int>("HostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("HostId"));

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IpAddr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBoot")
                        .HasColumnType("boolean");

                    b.Property<string>("MacAddr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("HostId");

                    b.HasIndex("UserId");

                    b.ToTable("PhyHost", (string)null);
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("HashSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Entity.VirtualHost", b =>
                {
                    b.Property<int>("VirtualHostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VirtualHostId"));

                    b.Property<int>("HostId")
                        .HasColumnType("integer");

                    b.Property<string>("IpAddr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBoot")
                        .HasColumnType("boolean");

                    b.Property<string>("VirtualHostName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VirtualHostId");

                    b.HasIndex("HostId");

                    b.ToTable("VirtaulHost", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");
                });

            modelBuilder.Entity("Entity.PhyHost", b =>
                {
                    b.HasOne("Entity.User", "User")
                        .WithMany("LinksHosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Entity.VirtualHost", b =>
                {
                    b.HasOne("Entity.PhyHost", "Host")
                        .WithMany("VirtualHosts")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("Entity.PhyHost", b =>
                {
                    b.Navigation("VirtualHosts");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Navigation("LinksHosts");
                });
#pragma warning restore 612, 618
        }
    }
}
