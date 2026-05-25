using AppCCL.DTOs;
using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodos();

        Task<ProductoDto> ObtenerPorId(int id);

        Task Crear(Producto producto);

        Task Actualizar(Producto producto);

        Task Eliminar(Producto producto);
    }
}
