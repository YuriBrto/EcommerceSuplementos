using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSuplementos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacoesController : ControllerBase
    {
        private readonly IAvaliacaoService _service;
        public AvaliacoesController(IAvaliacaoService service) => _service = service;

        [HttpGet("produto/{idProduto}")]
        public async Task<IActionResult> GetByProduto(int idProduto) =>
            Ok(await _service.GetByProdutoAsync(idProduto));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Avaliacao avaliacao)
        {
            var criada = await _service.CreateAsync(avaliacao);
            return CreatedAtAction(nameof(GetByProduto), new { idProduto = criada.IdProduto }, criada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
