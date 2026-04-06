using EcommerceSuplementos.Domain.DTOs;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoResponseDto> GetCarrinhoAsync(int idUsuario);
        Task<CarrinhoResponseDto> AddItemAsync(int idUsuario, ItemCarrinhoRequestDto dto);
        Task<CarrinhoResponseDto> UpdateItemAsync(int idUsuario, int idItemCarrinho, ItemCarrinhoRequestDto dto);
        Task<CarrinhoResponseDto> RemoveItemAsync(int idUsuario, int idItemCarrinho);
        Task ClearAsync(int idUsuario);
    }
}