using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.API.Models;

namespace PopularSeguros.API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cobertura> Coberturas { get; set; }

    public virtual DbSet<EstadoPoliza> EstadoPolizas { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<TipoPoliza> TipoPolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PK__Cliente__B4ADFE396944665F");

            entity.ToTable("Cliente");

            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.HasKey(e => e.IdCobertura).HasName("PK__Cobertur__1D5BFBDC59FB0610");

            entity.ToTable("Cobertura");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPoliza).HasName("PK__EstadoPo__6B580FCD4DF0D7BE");

            entity.ToTable("EstadoPoliza");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.NumeroPoliza).HasName("PK__Poliza__38B310205E00718F");

            entity.ToTable("Poliza");

            entity.Property(e => e.Aseguradora)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Popular Seguros");
            entity.Property(e => e.CedulaAsegurado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Eliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.FechaInclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.MontoAsegurado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Prima).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CedulaAseguradoNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.CedulaAsegurado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Poliza__CedulaAs__5535A963");

            entity.HasOne(d => d.IdCoberturaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdCobertura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Poliza__IdCobert__571DF1D5");

            entity.HasOne(d => d.IdEstadoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdEstadoPoliza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Poliza__IdEstado__5812160E");

            entity.HasOne(d => d.IdTipoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdTipoPoliza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Poliza__IdTipoPo__5629CD9C");
        });

        modelBuilder.Entity<TipoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdTipoPoliza).HasName("PK__TipoPoli__1BF2E5A7DA06EF9D");

            entity.ToTable("TipoPoliza");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF975DAFE46A");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.LoginUsuario, "UQ__Usuario__49AE9A22F180D303").IsUnique();

            entity.Property(e => e.LoginUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
