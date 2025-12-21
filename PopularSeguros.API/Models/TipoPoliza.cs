using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class TipoPoliza
{
    public int IdTipoPoliza { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
