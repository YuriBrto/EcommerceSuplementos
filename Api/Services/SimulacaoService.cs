using EcommerceSuplementos.Domain.Interfaces.Repositories;
using EcommerceSuplementos.Domain.Interfaces;
using static EcommerceSuplementos.Domain.Entity.DTOs.SimulaçãoDietaDto;
using static EcommerceSuplementos.Domain.Entity.DTOs.SimulaçãoDietaResponse;

namespace EcommerceSuplementos.Api.Services
{
    public class SimulacaoService : ISimulaçãoService
    {
        private readonly IProdutoRepository _produtoRepo;

        public SimulacaoService(IProdutoRepository produtoRepo) => _produtoRepo = produtoRepo;

        public async Task<SimulacaoDietaResponse> CalcularAsync(SimulacaoDietaRequest request)
        {
            var (fator, descricao) = request.Objetivo.ToLower() switch
            {
                "ganho" => (2.0f, "Para ganho de massa, recomendamos 2g de proteína por kg corporal."),
                "emagrecimento" => (1.6f, "Para emagrecimento com preservação muscular, recomendamos 1.6g por kg."),
                "manutencao" => (1.2f, "Para manutenção, recomendamos 1.2g de proteína por kg corporal."),
                _ => (1.2f, "Recomendação padrão: 1.2g de proteína por kg corporal.")
            };

            var proteina = request.Peso * fator;
            var todosProdutos = await _produtoRepo.GetAllAsync();

           
            var sugeridos = todosProdutos
                .Where(p => p.Categoria.ToLower().Contains("prote") ||
                            p.Nome.ToLower().Contains("whey") ||
                            p.Nome.ToLower().Contains("caseina"))
                .Take(3)
                .ToList();

            return new SimulacaoDietaResponse
            {
                ProteinaDiaria = MathF.Round(proteina, 1),
                Descricao = descricao,
                ProdutosSugeridos = sugeridos
            };
        }
    }
}
