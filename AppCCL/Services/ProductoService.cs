using AppCCL.DTOs;
using AppCCL.Interfaces;
using AppCCL.Models;
using AppCCL.Repository;
using Microsoft.EntityFrameworkCore;

namespace AppCCL.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodos()
        {
            var productos = await _repository.ObtenerTodos();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Stock = p.Stock
            });
        }

        public async Task<ProductoDto?> ObtenerPorId(int id)
        {
            var producto = await _repository.ObtenerPorId(id);

            if (producto == null)
                return null;

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Stock = producto.Stock
            };
        }

        public async Task Crear(CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Stock = dto.Stock,
                Fechacreacion = DateTime.Now
            };

            await _repository.Crear(producto);
        }

        public async Task<bool> Actualizar(int id, ActualizarProductoDto dto)
        {
            var productoDto = await _repository.ObtenerPorId(id);

            if (productoDto == null)
                return false;

            var producto = new Producto
            {
                Id = id,
                Nombre = dto.Nombre,
                Stock = dto.Stock
            };

            await _repository.Actualizar(producto);

            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var productoDto = await _repository.ObtenerPorId(id);

            if (productoDto == null)
                return false;

            var producto = new Producto
            {
                Id = productoDto.Id
            };

            await _repository.Eliminar(producto);

            return true;
        }
    }
}
