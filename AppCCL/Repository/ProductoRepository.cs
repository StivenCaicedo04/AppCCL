using AppCCL.Data;
using AppCCL.DTOs;
using AppCCL.Interfaces;
using AppCCL.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCCL.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<ProductoDto> ObtenerPorId(int id)
        {
            return await _context.Productos
                .Where(p => p.Id == id)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Stock = p.Stock,
                    Movimientos = p.MovimientosInventarios.Select(m => new MovimientoInventarioDto
                    {
                        Id = m.Id,
                        ProductoId = p.Id,
                        TipoMovimiento = m.TipoMovimiento,
                        Cantidad = m.Cantidad,
                        Fecha = m.FechaMovimiento
                    }).ToList()
                })
                .FirstOrDefaultAsync(); ;
                }

        public async Task Crear(Producto producto)
        {
            await _context.Productos.AddAsync(producto);

            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Producto producto)
        {
            _context.Productos.Update(producto);

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(Producto producto)
        {
            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();
        }
    }
}
