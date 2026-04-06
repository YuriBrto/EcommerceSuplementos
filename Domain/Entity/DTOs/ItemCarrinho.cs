using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceSuplementos.Domain.Entity
{
    public class ItemCarrinho
    {
        [Key]
        public int IdItemCarrinho { get; set; }

        [Required]
        public int IdCarrinho { get; set; }

        [Required]
        public int IdProduto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser pelo menos 1.")]
        public int Quantidade { get; set; }

        // Navigation
        [ForeignKey(nameof(IdCarrinho))]
        public Carrinho? Carrinho { get; set; }

        [ForeignKey(nameof(IdProduto))]
        public Produto? Produto { get; set; }
    }
}