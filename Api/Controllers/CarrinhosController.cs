using EcommerceSuplementos.Domain.DTOs;
using EcommerceSuplementos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSuplementos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhosController : ControllerBase
    {
        private readonly ICarrinhoService _service;

        public CarrinhosController(ICarrinhoService service)
        {
            _service = service;
        }

        // GET: api/carrinhos/{idUsuario}
        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> Get(int idUsuario)
        {
            var carrinho = await _service.GetCarrinhoAsync(idUsuario);
            return Ok(carrinho);
        }

        // POST: api/carrinhos/{idUsuario}/itens
        [HttpPost("{idUsuario}/itens")]
        public async Task<IActionResult> AddItem(int idUsuario, [FromBody] ItemCarrinhoRequestDto dto)
        {
            try
            {
                var carrinho = await _service.AddItemAsync(idUsuario, dto);
                return Ok(carrinho);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/carrinhos/{idUsuario}/itens/{idItemCarrinho}
        [HttpPut("{idUsuario}/itens/{idItemCarrinho}")]
        public async Task<IActionResult> UpdateItem(int idUsuario, int idItemCarrinho, [FromBody] ItemCarrinhoRequestDto dto)
        {
            try
            {
                var carrinho = await _service.UpdateItemAsync(idUsuario, idItemCarrinho, dto);
                return Ok(carrinho);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/carrinhos/{idUsuario}/itens/{idItemCarrinho}
        [HttpDelete("{idUsuario}/itens/{idItemCarrinho}")]
        public async Task<IActionResult> RemoveItem(int idUsuario, int idItemCarrinho)
        {
            try
            {
                var carrinho = await _service.RemoveItemAsync(idUsuario, idItemCarrinho);
                return Ok(carrinho);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // DELETE: api/carrinhos/{idUsuario}
        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> Clear(int idUsuario)
        {
            await _service.ClearAsync(idUsuario);
            return NoContent();
        }
    }
}