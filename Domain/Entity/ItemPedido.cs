using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceSuplementos.Domain.Entity
{
    public class ItemPedido
    {
        [Key]
        public int IdItemPedido { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido? Pedido { get; set; }

        [Required]
        public int IdProduto { get; set; }

        [ForeignKey("IdProduto")]
        public Produto? Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
    }
}