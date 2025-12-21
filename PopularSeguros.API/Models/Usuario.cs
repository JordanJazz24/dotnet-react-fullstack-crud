using System;
using System.Collections.Generic;

namespace PopularSeguros.API.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string LoginUsuario { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? NombreCompleto { get; set; }
}
