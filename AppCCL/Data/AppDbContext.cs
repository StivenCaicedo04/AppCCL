using Microsoft.EntityFrameworkCore;
using AppCCL.Models;

namespace AppCCL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<MovimientosInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Nombre).HasColumnName("nombre");
                entity.Property(p => p.Stock).HasColumnName("stock");
                entity.Property(p => p.Fechacreacion).HasColumnName("fechacreacion");
            });

            modelBuilder.Entity<MovimientosInventario>(entity =>
            {
                entity.ToTable("movimientosinventario");

                entity.HasKey(m => m.Id);

                entity.Property(m => m.Id).HasColumnName("id");
                entity.Property(m => m.ProductoId).HasColumnName("productoid");
                entity.Property(m => m.TipoMovimiento).HasColumnName("tipomovimiento");
                entity.Property(m => m.Cantidad).HasColumnName("cantidad");
                entity.Property(p => p.FechaMovimiento).HasColumnName("fechamovimiento");
            });

            modelBuilder.Entity<MovimientosInventario>()
                .HasOne(m => m.Producto)
                .WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(m => m.ProductoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
