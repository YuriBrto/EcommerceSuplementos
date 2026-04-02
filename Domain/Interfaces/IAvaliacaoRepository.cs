
using EcommerceSuplementos.Domain.Entity;

public interface IAvaliacaoRepository
{
    Task<IEnumerable<Avaliacao>> GetByProdutoAsync(int idProduto);
    Task<Avaliacao?> GetByIdAsync(int id);
    Task AddAsync(Avaliacao avaliacao);
    Task DeleteAsync(Avaliacao avaliacao);
    Task SaveAsync();
}