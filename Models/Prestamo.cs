using System;
using System.Collections.Generic;

namespace Biblioteca0_0.Models;

public partial class Prestamo
{
    public int Idprestamo { get; set; }

    public int? Idusuario { get; set; }

    public int? Idlibro { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public DateOnly? Fechadevo { get; set; }

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual Libro? IdlibroNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
