using System;
using System.Collections.Generic;
using DPA.Practicando.DOMAIN.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DPA.Practicando.DOMAIN.Infrastructure.Data;

public partial class ReservasDeportivasContext : DbContext
{
    public ReservasDeportivasContext()
    {
    }

    public ReservasDeportivasContext(DbContextOptions<ReservasDeportivasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Canchas> Canchas { get; set; }

    public virtual DbSet<Reservas> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canchas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Canchas__3214EC07C4DCCE60");

            entity.HasIndex(e => new { e.Tipo, e.Ubicacion }, "IX_Canchas_Tipo_Ubicacion");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.Ubicacion).HasMaxLength(150);
        });

        modelBuilder.Entity<Reservas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservas__3214EC0771C81FB8");

            entity.HasIndex(e => new { e.Fecha, e.CanchaId }, "IX_Reservas_Fecha_CanchaId");

            entity.Property(e => e.ClienteNombre).HasMaxLength(100);

            entity.HasOne(d => d.Cancha).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.CanchaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Cancha__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
