using AppCCL.DTOs;
using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ObtenerTodos();

        Task<Producto> ObtenerPorId(int id);

        Task Crear(CrearProductoDto dto);

        Task<bool> Actualizar(int id, ActualizarProductoDto dto);

        Task<bool> Eliminar(int id);
    }
}
