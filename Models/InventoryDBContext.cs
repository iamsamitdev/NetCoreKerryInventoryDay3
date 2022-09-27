using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetcoreKerryInventory.Models;

public partial class InventoryDBContext : DbContext
{
    public InventoryDBContext()
    {
    }

    public InventoryDBContext(DbContextOptions<InventoryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=SAMITZ50;Initial Catalog=ASPInventoryKerry;Persist Security Info=False;User ID=sa;Password=377040;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30; ");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.Property(e => e.ProductPicture)
                .HasMaxLength(1024)
                .IsUnicode(false);

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

            entity.Property(e => e.EmailID)
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
