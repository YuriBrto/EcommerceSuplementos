using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.Api.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repo;
        public PedidoService(IPedidoRepository repo) => _repo = repo;

        public async Task<IEnumerable<Pedido>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Pedido?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<IEnumerable<Pedido>> GetByUsuarioAsync(int idUsuario) =>
            await _repo.GetByUsuarioAsync(idUsuario);

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            pedido.DataCriacao = DateTime.UtcNow;
            pedido.Status = "Processando";
            await _repo.AddAsync(pedido);
            await _repo.SaveAsync();
            return pedido;
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var pedido = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");
            pedido.Status = status;
            await _repo.UpdateAsync(pedido);
            await _repo.SaveAsync();
        }
    }
}
