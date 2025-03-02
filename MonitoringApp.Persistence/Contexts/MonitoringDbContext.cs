using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitoringApp.Persistence.Entities;

namespace MonitoringApp.Persistence.Contexts;

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

    public virtual DbSet<Errors> Errors { get; set; }

    public virtual DbSet<Jobs> Jobs { get; set; }

    public virtual DbSet<Servicehealthlogs> Servicehealthlogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        foreach (var property in entityType.GetProperties())
        {
            if (property.ClrType == typeof(DateTime))
            {
                property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                    v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified)
                ));
            }
        }
    }
    
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

        modelBuilder.Entity<Errors>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("errors_pkey");

            entity.ToTable("errors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Errortype)
                .HasMaxLength(255)
                .HasColumnName("errortype");
            entity.Property(e => e.Statuscode).HasColumnName("statuscode");

            entity.HasOne(d => d.Application).WithMany(p => p.Errors)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("errors_applicationid_fkey");
        });

        modelBuilder.Entity<Jobs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Errormessage).HasColumnName("errormessage");
            entity.Property(e => e.Jobname)
                .HasMaxLength(255)
                .HasColumnName("jobname");
            entity.Property(e => e.Lastrun)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastrun");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Application).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("jobs_applicationid_fkey");
        });

        modelBuilder.Entity<Servicehealthlogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("servicehealthlogs_pkey");

            entity.ToTable("servicehealthlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applicationid).HasColumnName("applicationid");
            entity.Property(e => e.Checkedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("checkedat");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Errormessage).HasColumnName("errormessage");
            entity.Property(e => e.Errortype)
                .HasMaxLength(255)
                .HasColumnName("errortype");
            entity.Property(e => e.Responsetimems).HasColumnName("responsetimems");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Manual'::character varying")
                .HasColumnName("source");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Statuscode).HasColumnName("statuscode");

            entity.HasOne(d => d.Application).WithMany(p => p.Servicehealthlogs)
                .HasForeignKey(d => d.Applicationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("servicehealthlogs_applicationid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
