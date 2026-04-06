using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces.Repositories
{
    public interface ICarrinhoRepository
    {
        /// <summary>Retorna o carrinho do usuário com os itens e produtos incluídos.</summary>
        Task<Carrinho?> GetByUsuarioAsync(int idUsuario);

        Task AddAsync(Carrinho carrinho);
        Task AddItemAsync(ItemCarrinho item);
        Task UpdateItemAsync(ItemCarrinho item);
        Task DeleteItemAsync(ItemCarrinho item);
        Task<ItemCarrinho?> GetItemAsync(int idCarrinho, int idProduto);
        Task<ItemCarrinho?> GetItemByIdAsync(int idItemCarrinho);
        Task SaveAsync();
    }
}