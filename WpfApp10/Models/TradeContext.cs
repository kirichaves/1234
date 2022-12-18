﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ykrahenia.Models
{
    public partial class TradeContext : DbContext
    {
        public TradeContext()
        {
        }

        public TradeContext(DbContextOptions<TradeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-7MJS09NAH2K; Database=Trade;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDeliveryDate).HasColumnType("datetime");

                entity.HasMany(d => d.ProductArticleNumbers)
                    .WithMany(p => p.Orders)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderProduct",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductArticleNumber").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__OrderProd__Produ__2E1BDC42"),
                        r => r.HasOne<Order>().WithMany().HasForeignKey("OrderId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__OrderProd__Order__2D27B809"),
                        j =>
                        {
                            j.HasKey("OrderId", "ProductArticleNumber").HasName("PK__OrderPro__817A26629E339B61");

                            j.ToTable("OrderProduct");

                            j.IndexerProperty<int>("OrderId").HasColumnName("OrderID");

                            j.IndexerProperty<string>("ProductArticleNumber").HasMaxLength(100);
                        });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductArticleNumber)
                    .HasName("PK__Product__2EA7DCD5DE643141");

                entity.ToTable("Product");

                entity.Property(e => e.ProductArticleNumber).HasMaxLength(100);

                entity.Property(e => e.ProductCost).HasColumnType("decimal(19, 4)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserPatronymic).HasMaxLength(100);

                entity.Property(e => e.UserSurname).HasMaxLength(100);

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__UserRole__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
