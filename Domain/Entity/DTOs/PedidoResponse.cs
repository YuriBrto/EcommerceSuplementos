namespace EcommerceSuplementos.Domain.Entity.DTOs
{
    public class PedidoResponse
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemPedidoResponse> ItensPedido { get; set; }
    }

    public class ItemPedidoResponse
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
