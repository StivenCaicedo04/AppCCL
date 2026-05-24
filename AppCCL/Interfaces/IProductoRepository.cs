using AppCCL.Models;

namespace AppCCL.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodos();

        Task<Producto> ObtenerPorId(int id);

        Task Crear(Producto producto);

        Task Actualizar(Producto producto);

        Task Eliminar(Producto producto);
    }
}
