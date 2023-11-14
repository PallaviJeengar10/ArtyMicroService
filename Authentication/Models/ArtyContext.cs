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

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A35362700");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserRole).HasMaxLength(50);
        });

        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD31BC794");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });
    }
}
