using System.ComponentModel.DataAnnotations;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Avaliacao
    {
        [Key]
        public int IdAvaliacao { get; set; }

        public int IdProduto { get; set; }
        public int IdUsuario { get; set; }

        [Range(1, 5)]
        public int Nota { get; set; }

        public string? Comentario { get; set; }
    }
}