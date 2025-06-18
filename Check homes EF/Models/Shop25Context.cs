using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Check_homes_EF.Models;

public partial class Shop25Context : DbContext
{
    public Shop25Context()
    {
    }

    public Shop25Context(DbContextOptions<Shop25Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductLog> ProductLogs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Shop25;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0797CF8007");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F694F365FE").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07742A1687");

            entity.HasIndex(e => e.Name, "UQ__Cities__737584F663CF85A0").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07786BDF40");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trgAfterInsertProduct");
                    tb.HasTrigger("trgPreventPriceChange");
                });

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__2D27B809");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Products__Suppli__2E1BDC42");
        });

        modelBuilder.Entity<ProductLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductLog");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InsertedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07DAC20B28");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.City).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Suppliers__CityI__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
