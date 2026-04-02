
using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task AddAsync(Produto produto);

        Task UpdateAsync(Produto produto);
        Task DeleteAsync(Produto produto); 

        Task SaveAsync();
    }
}