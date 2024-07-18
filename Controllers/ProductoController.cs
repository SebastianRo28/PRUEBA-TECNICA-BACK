using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba.Service.DTO;
using prueba.Service.Service.interfaz;
using System.Threading;

namespace pruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;
        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProducts(int id, string nombre)
        {
            try
            {
                var response = await _service.GetProductosAsync(id, nombre);


                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAllProducts()
        {
            try
            {
                var response = await _service.GetAllProductosAsync();


                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPost]
        [Route("RegistrarProducto")]
        public async Task<IActionResult> RegistrarProducto([FromBody] ProductoDTO producto)
        {
            try
            {
                await _service.AddProductoAsync(producto);


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
