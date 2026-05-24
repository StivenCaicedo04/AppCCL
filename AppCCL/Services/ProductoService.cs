using AppCCL.DTOs;
using AppCCL.Interfaces;
using AppCCL.Models;

namespace AppCCL.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodos()
        {
            return await _repository.ObtenerTodos();
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _repository.ObtenerPorId(id);
        }

        public async Task Crear(CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Stock = dto.Stock
            };

            await _repository.Crear(producto);
        }

        public async Task<bool> Actualizar(int id, ActualizarProductoDto dto)
        {
            var producto = await _repository.ObtenerPorId(id);

            if (producto == null)
                return false;

            producto.Nombre = dto.Nombre;
            producto.Stock = dto.Stock;

            await _repository.Actualizar(producto);

            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await _repository.ObtenerPorId(id);

            if (producto == null)
                return false;

            await _repository.Eliminar(producto);

            return true;
        }
    }
}
