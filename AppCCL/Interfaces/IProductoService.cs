using AppCCL.DTOs;
using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerTodos();

        Task<ProductoDto> ObtenerPorId(int id);

        Task Crear(CrearProductoDto dto);

        Task<bool> Actualizar(int id, ActualizarProductoDto dto);

        Task<bool> Eliminar(int id);
    }
}
