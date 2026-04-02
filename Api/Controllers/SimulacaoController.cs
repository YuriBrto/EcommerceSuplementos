using EcommerceSuplementos.Domain.Entity.DTOs;
using EcommerceSuplementos.Api.Services;
using EcommerceSuplementos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static EcommerceSuplementos.Domain.Entity.DTOs.SimulaçãoDietaDto;

namespace EcommerceSuplementos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulacaoController : ControllerBase
    {
        private readonly ISimulaçãoService _service;

        public SimulacaoController(ISimulaçãoService service) => _service = service;

        [HttpPost("dieta")]
        public async Task<IActionResult> Calcular([FromBody] SimulacaoDietaRequest request)
        {
            if (request.Peso <= 0)
                return BadRequest("Peso inválido.");

            var resultado = await _service.CalcularAsync(request);
            return Ok(resultado);
        }
    }
}