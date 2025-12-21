using System.ComponentModel.DataAnnotations;

namespace PopularSeguros.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50)]
        public string Usuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres")]
        public string Password { get; set; } = null!;
    }
}