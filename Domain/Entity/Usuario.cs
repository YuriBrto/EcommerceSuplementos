using System.ComponentModel.DataAnnotations;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string EmailUsuario { get; set; }

        [Required]
        public string Senha { get; set; }

        // Dados fitness 
        public double? Peso { get; set; }
        public double? Altura { get; set; }

        [MaxLength(50)]
        public string Objetivo { get; set; } // Bulking, Cutting, etc.
    }
}