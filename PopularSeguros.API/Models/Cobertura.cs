using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class Cobertura
{
    public int IdCobertura { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
