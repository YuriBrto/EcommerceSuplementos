using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSuplementos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        public PedidoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Pedido>> GetAllAsync() =>
            await _context.Pedidos.Include(p => p.Usuario).ToListAsync();

        public async Task<Pedido?> GetByIdAsync(int id) =>
            await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.ItensPedido)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

        public async Task<IEnumerable<Pedido>> GetByUsuarioAsync(int idUsuario) =>
            await _context.Pedidos
                .Where(p => p.IdUsuario == idUsuario)
                .Include(p => p.ItensPedido)
                .ToListAsync();

        public async Task AddAsync(Pedido pedido) =>
            await _context.Pedidos.AddAsync(pedido);

        public async Task UpdateAsync(Pedido pedido) =>
            _context.Pedidos.Update(pedido);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
