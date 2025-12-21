using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class Cliente
{
    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string TipoPersona { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
