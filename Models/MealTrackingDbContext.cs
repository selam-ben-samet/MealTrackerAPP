using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MealTrackerAPP.Models;

public partial class MealTrackingDbContext : DbContext
{
    public MealTrackingDbContext()
    {
    }

    public MealTrackingDbContext(DbContextOptions<MealTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<PhysicalActivity> PhysicalActivities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WaterIntake> WaterIntakes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MealTrackingDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meals__3213E83F3C50F26D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PortionSize).HasColumnName("portion_size");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Meals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull) // Or as needed
                .HasConstraintName("FK__Meals__user_id__286302EC");
        });

        modelBuilder.Entity<PhysicalActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Physical__3213E83F123472D2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(100)
                .HasColumnName("activity_type");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.PhysicalActivities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull) // Or as needed
                .HasConstraintName("FK__PhysicalA__user___300424B4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FE5E044F8");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572C602F793").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<WaterIntake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WaterInt__3213E83F4DE1610B");

            entity.ToTable("WaterIntake");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.WaterIntakes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull) // Or as needed
                .HasConstraintName("FK__WaterInta__user___2C3393D0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder); }
