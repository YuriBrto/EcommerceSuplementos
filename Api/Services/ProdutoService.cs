using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces.Repositories;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.Api.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;
        public ProdutoService(IProdutoRepository repo) => _repo = repo;

        public async Task<IEnumerable<Produto>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<Produto?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<Produto> CreateAsync(Produto produto)
        {
            await _repo.AddAsync(produto);
            await _repo.SaveAsync();
            return produto;
        }

        public async Task UpdateAsync(int id, Produto produto)
        {
            var existente = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Produto não encontrado.");

            existente.Nome = produto.Nome;
            existente.Marca = produto.Marca;
            existente.Preco = produto.Preco;
            existente.Descricao = produto.Descricao;
            existente.Categoria = produto.Categoria;
            existente.ContemCafeina = produto.ContemCafeina;
            existente.Estoque = produto.Estoque;
            existente.ImagemUrl = produto.ImagemUrl;

            await _repo.UpdateAsync(existente);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Produto não encontrado.");
            await _repo.DeleteAsync(produto);
            await _repo.SaveAsync();
        }
    }
}
