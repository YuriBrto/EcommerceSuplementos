using static EcommerceSuplementos.Domain.Entity.DTOs.SimulaçãoDietaDto;
using static EcommerceSuplementos.Domain.Entity.DTOs.SimulaçãoDietaResponse;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface ISimulaçãoService
    {
        Task<SimulacaoDietaResponse> CalcularAsync(SimulacaoDietaRequest request);

    }
}
