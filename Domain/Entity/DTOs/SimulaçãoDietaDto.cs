namespace EcommerceSuplementos.Domain.Entity.DTOs
{
    public class SimulaçãoDietaDto
    {
        public class SimulacaoDietaRequest
        {
            public float Peso { get; set; }
            public string Objetivo { get; set; } // "ganho", "emagrecimento", "manutencao"
        }
    }
}
