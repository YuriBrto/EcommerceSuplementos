using Microsoft.AspNetCore.Mvc;
using EcommerceSuplementos.Infrastructure.Data;
using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var produtos = await _service.GetAllAsync();
            return Ok(produtos);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _service.GetByIdAsync(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            var criado = await _service.CreateAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdProduto }, criado);
        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                await _service.UpdateAsync(id, produto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}