using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task.API.Entities;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("ti_pkey");

            entity.ToTable("task");

            entity.Property(e => e.TaskId)
                .ValueGeneratedNever()
                .HasColumnName("task_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PlannedEnd).HasColumnName("planned_end");
            entity.Property(e => e.PlannedStart).HasColumnName("planned_start");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");
            entity.Property(e => e.TaskTypeId).HasColumnName("task_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("t_tsi_fkey");

            entity.HasOne(d => d.TaskType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("t_tti_fkey");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.HasKey(e => e.TaskStatusId).HasName("etsi_pkey");

            entity.ToTable("task_status", "enum");

            entity.Property(e => e.TaskStatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("task_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.HasKey(e => e.TaskTypeId).HasName("etti_pkey");

            entity.ToTable("task_type", "enum");

            entity.Property(e => e.TaskTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("task_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
