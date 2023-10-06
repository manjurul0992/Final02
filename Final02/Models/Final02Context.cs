using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final02.Models
{
    public partial class Final02Context : DbContext
    {
        public Final02Context()
        {
        }

        public Final02Context(DbContextOptions<Final02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Format> Formats { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<SeriesEntry> SeriesEntries { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Format>(entity =>
            {
                entity.Property(e => e.FormatName).HasMaxLength(100);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.PlayerName).HasMaxLength(100);
            });

            modelBuilder.Entity<SeriesEntry>(entity =>
            {
                entity.ToTable("SeriesEntry");

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.SeriesEntries)
                    .HasForeignKey(d => d.FormatId)
                    .HasConstraintName("FK__SeriesEnt__Format");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SeriesEntries)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK__SeriesEnt__Player");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
