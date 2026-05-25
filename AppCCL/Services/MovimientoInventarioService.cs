using AppCCL.DTOs;
using AppCCL.Interfaces;
using AppCCL.Models;
using AppCCL.Repository;

namespace AppCCL.Services
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly IMovimientoInventarioRepository _movimientoRepository;
        private readonly IProductoRepository _productoRepository;

        public MovimientoInventarioService(
            IMovimientoInventarioRepository movimientoRepository,
            IProductoRepository productoRepository)
        {
            _movimientoRepository = movimientoRepository;
            _productoRepository = productoRepository;
        }

        public async Task<bool> RegistrarMovimiento(MovimientoInventarioDto dto)
        {
            var productoDto = await _productoRepository.ObtenerPorId(dto.ProductoId);

            if (productoDto == null)
                return false;

            if (dto.TipoMovimiento?.ToUpper() == "ENTRADA")
            {
                productoDto.Stock += dto.Cantidad;
            }
            else if (dto.TipoMovimiento?.ToUpper() == "SALIDA")
            {
                if (productoDto.Stock < dto.Cantidad)
                    return false;

                productoDto.Stock -= dto.Cantidad;
            }

            var movimiento = new MovimientosInventario
            {
                ProductoId = dto.ProductoId,
                TipoMovimiento = dto.TipoMovimiento,
                Cantidad = dto.Cantidad,
                FechaMovimiento = DateTime.UtcNow
            };

            var producto = new Producto
            {
                Id = productoDto.Id,
                Nombre = productoDto.Nombre,
                Stock = productoDto.Stock
            };


            await _productoRepository.Actualizar(producto);
            await _movimientoRepository.Crear(movimiento);

            return true;
        }

        public async Task<IEnumerable<MovimientoInventarioDto>> ObtenerMovimientos()
        {
            var movimientos = await _movimientoRepository.ObtenerTodos();

            return movimientos.Select(m => new MovimientoInventarioDto
            {
                Id = m.Id,
                ProductoId = m.ProductoId,
                Cantidad = m.Cantidad,
                TipoMovimiento = m.TipoMovimiento,
                Fecha = m.FechaMovimiento
            });
        }
    }
}
