using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces.Repositories;
using EcommerceSuplementos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSuplementos.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Produto>> GetAllAsync() =>
            await _context.Produtos.ToListAsync();

        public async Task<Produto?> GetByIdAsync(int id) =>
            await _context.Produtos.FindAsync(id);

        public async Task AddAsync(Produto produto) =>
            await _context.Produtos.AddAsync(produto);

        public Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            return Task.CompletedTask;
        }

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
