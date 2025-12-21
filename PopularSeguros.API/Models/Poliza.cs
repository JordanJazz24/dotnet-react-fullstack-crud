using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class Poliza
{
    public int NumeroPoliza { get; set; }

    public string CedulaAsegurado { get; set; } = null!;

    public int IdTipoPoliza { get; set; }

    public int IdCobertura { get; set; }

    public int IdEstadoPoliza { get; set; }

    public decimal MontoAsegurado { get; set; }

    public decimal Prima { get; set; }

    public string Aseguradora { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public DateTime FechaInclusion { get; set; }

    public bool? Eliminado { get; set; }

    public virtual Cliente CedulaAseguradoNavigation { get; set; } = null!;

    public virtual Cobertura IdCoberturaNavigation { get; set; } = null!;

    public virtual EstadoPoliza IdEstadoPolizaNavigation { get; set; } = null!;

    public virtual TipoPoliza IdTipoPolizaNavigation { get; set; } = null!;
}
