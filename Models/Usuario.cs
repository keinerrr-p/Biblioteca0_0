using System;
using System.Collections.Generic;

namespace Biblioteca0_0.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string TipoDoc { get; set; } = null!;

    public string NumDoc { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;
    public string Contraseña { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public enum TipoDocumentoEnum
{
    TI,
    CC,
    PPT
}

}

