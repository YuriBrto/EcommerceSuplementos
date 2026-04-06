using EcommerceSuplementos.Api.DTOs;
using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Entity.DTOs;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoResponse>> GetAllAsync();
        Task<PedidoResponse?> GetByIdAsync(int id);
        Task<IEnumerable<PedidoResponse>> GetByUsuarioAsync(int idUsuario);
        Task<PedidoResponse> CreateAsync(CriarPedidoRequest request);
        Task UpdateStatusAsync(int id, string status);
    }
}
