using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {
        private IProductoFlujo _productoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Actualizar(Guid Id, ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Actualizar(Id, producto);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Agregar(producto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _productoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _productoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerPorID(Guid Id)
        {
            var resultado = await _productoFlujo.ObtenerPorID(Id);
            return Ok(resultado);
        }
    }
}
