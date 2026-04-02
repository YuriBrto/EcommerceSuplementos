using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.Api.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _repo;
        public AvaliacaoService(IAvaliacaoRepository repo) => _repo = repo;

        public async Task<IEnumerable<Avaliacao>> GetByProdutoAsync(int idProduto) =>
            await _repo.GetByProdutoAsync(idProduto);

        public async Task<Avaliacao> CreateAsync(Avaliacao avaliacao)
        {
            await _repo.AddAsync(avaliacao);
            await _repo.SaveAsync();
            return avaliacao;
        }

        public async Task DeleteAsync(int id)
        {
            var avaliacao = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Avaliação não encontrada.");
            await _repo.DeleteAsync(avaliacao);
            await _repo.SaveAsync();
        }
    }
}
