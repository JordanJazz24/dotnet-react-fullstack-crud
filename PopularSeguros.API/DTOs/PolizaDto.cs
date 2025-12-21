namespace PopularSeguros.API.DTOs
{
    public class PolizaDto
    {
        public int NumeroPoliza { get; set; }
        public string CedulaAsegurado { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;

        public int IdTipoPoliza { get; set; }
        public string NombreTipoPoliza { get; set; } = null!;

        public int IdCobertura { get; set; }
        public string NombreCobertura { get; set; } = null!;

        public int IdEstadoPoliza { get; set; }
        public string NombreEstado { get; set; } = null!;

        public decimal MontoAsegurado { get; set; }
        public decimal Prima { get; set; }
        public DateTime FechaVencimiento { get; set; }

    
        public string Aseguradora { get; set; } = null!;
        public DateTime FechaInclusion { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}