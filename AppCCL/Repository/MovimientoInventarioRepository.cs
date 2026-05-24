using AppCCL.Data;
using AppCCL.Interfaces;
using AppCCL.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCCL.Repository
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly AppDbContext _context;

        public MovimientoInventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Crear(MovimientosInventario movimiento)
        {
            await _context.MovimientosInventario.AddAsync(movimiento);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovimientosInventario>> ObtenerTodos()
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .ToListAsync();
        }
    }
}
