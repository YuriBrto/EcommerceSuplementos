namespace EcommerceSuplementos.Domain.DTOs
{
    // ── Entrada ────────────────────────────────────────────

    /// <summary>Adiciona ou atualiza um item no carrinho.</summary>
    public class ItemCarrinhoRequestDto
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }

    // ── Saída ──────────────────────────────────────────────

    /// <summary>Representa um item dentro do resumo do carrinho.</summary>
    public class ItemCarrinhoResponseDto
    {
        public int IdItemCarrinho { get; set; }
        public int IdProduto { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string? ImagemUrl { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal => PrecoUnitario * Quantidade;
    }

    /// <summary>Resumo completo do carrinho retornado pela API.</summary>
    public class CarrinhoResponseDto
    {
        public int IdCarrinho { get; set; }
        public int IdUsuario { get; set; }
        public List<ItemCarrinhoResponseDto> Itens { get; set; } = new();
        public decimal Total => Itens.Sum(i => i.Subtotal);
        public int TotalItens => Itens.Sum(i => i.Quantidade);
    }
}