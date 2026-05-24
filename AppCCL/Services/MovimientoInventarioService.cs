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
            var producto = await _productoRepository.ObtenerPorId(dto.ProductoId);

            if (producto == null)
                return false;

            if (dto.TipoMovimiento.ToUpper() == "ENTRADA")
            {
                producto.Stock += dto.Cantidad;
            }
            else if (dto.TipoMovimiento.ToUpper() == "SALIDA")
            {
                if (producto.Stock < dto.Cantidad)
                    return false;

                producto.Stock -= dto.Cantidad;
            }

            var movimiento = new MovimientosInventario
            {
                ProductoId = dto.ProductoId,
                TipoMovimiento = dto.TipoMovimiento,
                Cantidad = dto.Cantidad
            };

            await _productoRepository.Actualizar(producto);
            await _movimientoRepository.Crear(movimiento);

            return true;
        }

        public async Task<IEnumerable<MovimientosInventario>> ObtenerMovimientos()
        {
            return await _movimientoRepository.ObtenerTodos();
        }
    }
}
