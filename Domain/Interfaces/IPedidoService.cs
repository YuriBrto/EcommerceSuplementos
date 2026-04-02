using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetByUsuarioAsync(int idUsuario);
        Task<Pedido> CreateAsync(Pedido pedido);
        Task UpdateStatusAsync(int id, string status);
    }
}
