using System.ComponentModel.DataAnnotations;

namespace PopularSeguros.API.DTOs
{
    public class PolizaCreateDto
    {
        [Required(ErrorMessage = "La cédula del asegurado es obligatoria")]
        [StringLength(20)]
        public string CedulaAsegurado { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de póliza válido")]
        public int IdTipoPoliza { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una cobertura válida")]
        public int IdCobertura { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido")]
        public int IdEstadoPoliza { get; set; }

        [Required]
        [Range(0.01, 999999999, ErrorMessage = "El monto asegurado debe ser mayor a 0")]
        public decimal MontoAsegurado { get; set; }

        [Required]
        [Range(0.01, 999999999, ErrorMessage = "La prima debe ser mayor a 0")]
        public decimal Prima { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [StringLength(100)]
        public string Aseguradora { get; set; } = "Popular Seguros";
    }
}