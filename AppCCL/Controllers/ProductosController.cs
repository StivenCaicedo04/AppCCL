using AppCCL.DTOs;
using AppCCL.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _service.ObtenerTodos();

            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProductoDto dto)
        {
            await _service.Crear(dto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarProductoDto dto)
        {
            var resultado = await _service.Actualizar(id, dto);

            if (!resultado)
                return NotFound();

            return Ok();
        }

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
