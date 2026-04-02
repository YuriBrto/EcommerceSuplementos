using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task<Produto> CreateAsync(Produto produto);
        Task UpdateAsync(int id, Produto produto);
        Task DeleteAsync(int id);
    }
}
