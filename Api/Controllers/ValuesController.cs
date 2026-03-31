using Microsoft.AspNetCore.Mvc;
using EcommerceSuplementos.Infrastructure.Data;
using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        // POST: api/produtos
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = produto.IdProduto }, produto);
        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produto)
        {
            var produtoExistente = _context.Produtos.Find(id);

            if (produtoExistente == null)
                return NotFound();

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Marca = produto.Marca;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Categoria = produto.Categoria;
            produtoExistente.ContemCafeina = produto.ContemCafeina;
            produtoExistente.Estoque = produto.Estoque;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}