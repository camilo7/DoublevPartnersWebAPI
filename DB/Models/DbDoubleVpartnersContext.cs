using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DB.Models;

public partial class DbDoubleVpartnersContext : DbContext
{
    public DbDoubleVpartnersContext()
    {
    }

    public DbDoubleVpartnersContext(DbContextOptions<DbDoubleVpartnersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=CAMILO\\SQLEXPRESS; Database=DbDoubleVPartners; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC07736AF609");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(61)
                .IsUnicode(false)
                .HasComputedColumnSql("(([Names]+' ')+[LastNames])", false);
            entity.Property(e => e.Identification).HasComputedColumnSql("(([IdentificationNumber]+' ')+[IdentificationType])", false);
            entity.Property(e => e.IdentificationType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastNames)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Names)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07793A47EB");

            entity.ToTable("User");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Pass)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("UserName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
