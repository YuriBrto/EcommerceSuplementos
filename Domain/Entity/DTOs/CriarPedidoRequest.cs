namespace EcommerceSuplementos.Api.DTOs
{
    public class CriarPedidoRequest
    {
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Processando";
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public List<ItemPedidoRequest> ItensPedido { get; set; } = new();
    }

    public class ItemPedidoRequest
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}