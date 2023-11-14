using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace Arty.Models;

public partial class ArtyContext : DbContext
{
    public ArtyContext()
    {
    }

    public ArtyContext(DbContextOptions<ArtyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<WishList> WishLists { get; set; }

    public virtual DbSet<WishListItem> WishListItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797403469AD");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2AE8A9C086");

            entity.Property(e => e.CartItemId).HasColumnName("CartItemID");
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItems__CartI__4D94879B");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFDC6FF50C");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CC55FD0F7");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__46E78A0C");

        });

        modelBuilder.Entity<WishList>(entity =>
        {
            entity.HasKey(e => e.WishListId).HasName("PK__WishList__E41F87A7D91FF381");

            entity.ToTable("WishList");

            entity.Property(e => e.WishListId).HasColumnName("WishListID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<WishListItem>(entity =>
        {
            entity.HasKey(e => e.WishListItemId).HasName("PK__WishList__DAC208299FAD83A6");

            entity.Property(e => e.WishListItemId).HasColumnName("WishListItemID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.WishListId).HasColumnName("WishListID");
            entity.HasOne(d => d.WishList).WithMany(p => p.WishListItems)
                .HasForeignKey(d => d.WishListId)
                .HasConstraintName("FK__WishListI__WishL__5441852A");
        });
    }
}
