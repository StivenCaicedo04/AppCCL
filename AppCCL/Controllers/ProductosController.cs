using AppCCL.DTOs;
using AppCCL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCCL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _service.ObtenerTodos();

            return Ok(productos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Crear(CrearProductoDto dto)
        {
            await _service.Crear(dto);

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarProductoDto dto)
        {
            var resultado = await _service.Actualizar(id, dto);

            if (!resultado)
                return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.Eliminar(id);

            if (!resultado)
                return NotFound();

            return Ok();
        }
    }
}
