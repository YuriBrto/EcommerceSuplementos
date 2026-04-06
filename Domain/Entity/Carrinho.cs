using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Carrinho
    {
        [Key]
        public int IdCarrinho { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey(nameof(IdUsuario))]
        public Usuario? Usuario { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }
}