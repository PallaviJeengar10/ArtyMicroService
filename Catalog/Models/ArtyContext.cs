using System;
using System.Collections.Generic;
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

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<SubCategory> SubCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B04468055");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

       
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDECD06F17");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductDescription).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__Products__SubCat__412EB0B6");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK__SubCateg__26BE5BF9A9C4A67F");

            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.SubCategoryName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__SubCatego__Categ__3E52440B");
        });
    }
}
