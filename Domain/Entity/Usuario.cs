using System.ComponentModel.DataAnnotations;

namespace EcommerceSuplementos.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailUsuario { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        // Dados fitness 
        public double? Peso { get; set; }
        public double? Altura { get; set; }

        [MaxLength(50)]
        public string? Objetivo { get; set; } // Bulking, Cutting, etc.
    }
}