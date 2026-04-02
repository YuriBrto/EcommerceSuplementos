namespace EcommerceSuplementos.Domain.Entity.DTOs
{
    public class SimulaçãoDietaResponse
    {
        public class SimulacaoDietaResponse
        {
            public float ProteinaDiaria { get; set; }       
            public string Descricao { get; set; }           
            public IEnumerable<Produto> ProdutosSugeridos { get; set; }
        }
    }
}
