using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace User.API.Entities;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("eg_pkey");

            entity.ToTable("gender", "enum");

            entity.Property(e => e.GenderId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("gender_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("ua_pkey");

            entity.ToTable("user_account");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("last_login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegisterAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("register_at");

            entity.HasOne(d => d.Gender).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ua_eg_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
