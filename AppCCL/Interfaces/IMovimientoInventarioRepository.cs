using AppCCL.DTOs;
using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IMovimientoInventarioRepository
    {
        Task Crear(MovimientosInventario movimiento);

        Task<IEnumerable<MovimientosInventario>> ObtenerTodos();
    }
}
