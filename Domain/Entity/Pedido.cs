using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = "Processando";

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

    }
}