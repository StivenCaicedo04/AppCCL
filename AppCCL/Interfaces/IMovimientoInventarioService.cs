using AppCCL.DTOs;
using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IMovimientoInventarioService
    {
        Task<bool> RegistrarMovimiento(MovimientoInventarioDto dto);

        Task<IEnumerable<MovimientosInventario>> ObtenerMovimientos();
    }
}
