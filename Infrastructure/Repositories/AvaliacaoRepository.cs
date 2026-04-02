using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSuplementos.Infrastructure.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly AppDbContext _context;
        public AvaliacaoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Avaliacao>> GetByProdutoAsync(int idProduto) =>
            await _context.Avaliacoes
                .Where(a => a.IdProduto == idProduto)
                .ToListAsync();

        public async Task<Avaliacao?> GetByIdAsync(int id) =>
            await _context.Avaliacoes.FindAsync(id);

        public async Task AddAsync(Avaliacao avaliacao) =>
            await _context.Avaliacoes.AddAsync(avaliacao);

        public async Task DeleteAsync(Avaliacao avaliacao) =>
            _context.Avaliacoes.Remove(avaliacao);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
