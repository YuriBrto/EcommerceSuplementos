using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces.Repositories;
using EcommerceSuplementos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSuplementos.Infrastructure.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoRepository(AppDbContext context) => _context = context;

        public async Task<Carrinho?> GetByUsuarioAsync(int idUsuario) =>
            await _context.Carrinhos
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

        public async Task AddAsync(Carrinho carrinho) =>
            await _context.Carrinhos.AddAsync(carrinho);

        public async Task AddItemAsync(ItemCarrinho item) =>
            await _context.ItensCarrinho.AddAsync(item);

        public Task UpdateItemAsync(ItemCarrinho item)
        {
            _context.ItensCarrinho.Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteItemAsync(ItemCarrinho item)
        {
            _context.ItensCarrinho.Remove(item);
            return Task.CompletedTask;
        }

        public async Task<ItemCarrinho?> GetItemAsync(int idCarrinho, int idProduto) =>
            await _context.ItensCarrinho
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(i => i.IdCarrinho == idCarrinho && i.IdProduto == idProduto);

        public async Task<ItemCarrinho?> GetItemByIdAsync(int idItemCarrinho) =>
            await _context.ItensCarrinho
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(i => i.IdItemCarrinho == idItemCarrinho);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}