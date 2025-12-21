using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class EstadoPoliza
{
    public int IdEstadoPoliza { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
