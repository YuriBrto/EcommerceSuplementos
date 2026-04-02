using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSuplementos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;
        public PedidosController(IPedidoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);
            return pedido == null ? NotFound() : Ok(pedido);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario) =>
            Ok(await _service.GetByUsuarioAsync(idUsuario));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            var criado = await _service.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdPedido }, criado);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            await _service.UpdateStatusAsync(id, status);
            return NoContent();
        }
    }
}
