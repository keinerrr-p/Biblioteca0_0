using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca0_0.Models;
using Biblioteca0_0.Data;

namespace Biblioteca0_0.Controllers;

public class PrestamoController : Controller
{
    public readonly Biblioteca01Context _context;

    public PrestamoController(Biblioteca01Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult register()
    {
        ViewBag.ListaPrestamos = _context.Prestamos
            .Include(p => p.IdlibroNavigation)
            .Include(p => p.IdusuarioNavigation)
            .ToList();

        ViewBag.ListaLibros = _context.Libros.ToList();
        ViewBag.ListaUsuarios = _context.Usuarios.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult register(Prestamo nuevo)
    {
        if (ModelState.IsValid)
        {
            nuevo.Fecha = DateTime.Now;
            _context.Prestamos.Add(nuevo);
            _context.SaveChanges();
            return RedirectToAction("register");
        }

        ViewBag.ListaPrestamos = _context.Prestamos
            .Include(p => p.IdlibroNavigation)
            .Include(p => p.IdusuarioNavigation)
            .ToList();
        ViewBag.ListaLibros = _context.Libros.ToList();
        ViewBag.ListaUsuarios = _context.Usuarios.ToList();
        return View(nuevo);
    }

    public IActionResult Buscar(string? codigo)
    {
        if (codigo == null) return View("Buscar");

        var prestamo = _context.Prestamos
            .Include(p => p.IdlibroNavigation)
            .Include(p => p.IdusuarioNavigation)
            .FirstOrDefault(p => p.Codigo == codigo);

        if (prestamo == null) return View("Buscar");

        return View(prestamo);
    }

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null) return NotFound();
        var prestamo = _context.Prestamos.FirstOrDefault(p => p.Idprestamo == id);
        if (prestamo == null) return NotFound();

        ViewBag.ListaLibros = _context.Libros.ToList();
        ViewBag.ListaUsuarios = _context.Usuarios.ToList();
        return View(prestamo);
    }

    [HttpPost]
    public IActionResult Editar(Prestamo actualizado)
    {
        if (ModelState.IsValid)
        {
            _context.Prestamos.Update(actualizado);
            _context.SaveChanges();
            return RedirectToAction("Buscar", new { id = actualizado.Idprestamo });
        }
        ViewBag.ListaLibros = _context.Libros.ToList();
        ViewBag.ListaUsuarios = _context.Usuarios.ToList();
        return View(actualizado);
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        var prestamo = _context.Prestamos.FirstOrDefault(p => p.Idprestamo == id);
        if (prestamo != null)
        {
            _context.Prestamos.Remove(prestamo);
            _context.SaveChanges();
        }
        return RedirectToAction("register");
    }
}
