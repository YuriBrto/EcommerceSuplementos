using System.ComponentModel.DataAnnotations;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Marca { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        [MaxLength(50)]
        public string Categoria { get; set; } // Whey, Creatina...

        public bool ContemCafeina { get; set; }

        public int Estoque { get; set; }
    }
}