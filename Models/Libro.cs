using System;
using System.Collections.Generic;

namespace Biblioteca0_0.Models;

public partial class Libro
{
    public int Idlibro { get; set; }

    public string? Codlibro { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public DateTime? Publicacion { get; set; } = DateTime.Now;

    public string? Genero { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
