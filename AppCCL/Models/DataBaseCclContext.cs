using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppCCL.Models;

public partial class DataBaseCclContext : DbContext
{
    public DataBaseCclContext()
    {
    }

    public DataBaseCclContext(DbContextOptions<DataBaseCclContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MovimientosInventario> Movimientosinventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DataBaseCCL;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movimientosinventario_pkey");

            entity.ToTable("movimientosinventario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechamovimiento");
            entity.Property(e => e.ProductoId).HasColumnName("productoid");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(20)
                .HasColumnName("tipomovimiento");

            entity.HasOne(d => d.Producto).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_producto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
