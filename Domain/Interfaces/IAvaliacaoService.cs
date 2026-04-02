using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<IEnumerable<Avaliacao>> GetByProdutoAsync(int idProduto);
        Task<Avaliacao> CreateAsync(Avaliacao avaliacao);
        Task DeleteAsync(int id);
    }
}
