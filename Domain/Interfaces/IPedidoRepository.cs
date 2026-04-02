
using EcommerceSuplementos.Domain.Entity;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(int id);
    Task<IEnumerable<Pedido>> GetByUsuarioAsync(int idUsuario);
    Task AddAsync(Pedido pedido);
    Task UpdateAsync(Pedido pedido);
    Task SaveAsync();
}