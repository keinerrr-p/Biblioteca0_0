using System;
using System.Collections.Generic;

namespace Biblioteca0_0.Models;

public partial class Devolucion
{
    public int Iddevo { get; set; }

    public int? Idprestamo { get; set; }

    public DateTime? Fechadevo { get; set; }

    public string? Estado { get; set; }

    public string? Multa { get; set; }

    public decimal? Monto { get; set; }

    public virtual Prestamo? IdprestamoNavigation { get; set; }
}
