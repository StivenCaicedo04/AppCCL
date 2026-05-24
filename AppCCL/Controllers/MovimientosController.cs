using AppCCL.DTOs;
using AppCCL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppCCL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoInventarioService _service;

        public MovimientosController(IMovimientoInventarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento(MovimientoInventarioDto dto)
        {
            var resultado = await _service.RegistrarMovimiento(dto);

            if (!resultado)
                return BadRequest("Stock insuficiente o producto no existe");

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMovimientos()
        {
            var movimientos = await _service.ObtenerMovimientos();

            return Ok(movimientos);
        }
    }
}
