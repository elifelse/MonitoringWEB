using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MonitoringApp.Models;

public partial class MonitoringDbContext : DbContext
{
    public MonitoringDbContext()
    {
    }

    public MonitoringDbContext(DbContextOptions<MonitoringDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applications> Applications { get; set; }

    public virtual DbSet<Servicehealthlogs> Servicehealthlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=monitoringdb;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("applications_pkey");

            entity.ToTable("applications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<Servicehealthlogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("servicehealthlogs_pkey");

            entity.ToTable("servicehealthlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Errormessage).HasColumnName("errormessage");
            entity.Property(e => e.Responsetimems).HasColumnName("responsetimems");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Application).WithMany(p => p.Servicehealthlogs)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("servicehealthlogs_applicationid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
