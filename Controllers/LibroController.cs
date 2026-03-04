using Microsoft.AspNetCore.Mvc;
using Biblioteca0_0.Models;
using Biblioteca0_0.Data;

namespace Biblioteca0_0.Controllers;

public class LibroController : Controller
{
    public readonly Biblioteca01Context _context;

    public LibroController(Biblioteca01Context context)
        {
           _context = context;
        }

        [HttpGet]
        public IActionResult register()
        {
            ViewBag.listalibros = _context.Libros.ToList();
            return View();
        }

        public IActionResult Buscar(string codlibro)
        {
            var libro = _context.Libros.FirstOrDefault(p => p.Codlibro == codlibro);

            if (libro == null)
                return View("buscar"); 

            return View(libro);
        }
        [HttpGet]
        public IActionResult Editar(string id)
        {
            var libros = _context.Libros.FirstOrDefault(l => l.Codlibro == id);
            if (libros == null) return NotFound();
            
            return View(libros); 
        }

        [HttpPost]

        public IActionResult register(Libro nuevolibro)
    {
        if (ModelState.IsValid)
        {
            _context.Libros.Add(nuevolibro);
            _context.SaveChanges();

            return RedirectToAction("Registro","Usuario");
        }
         var listaLibros = _context.Libros.ToList();
        return View(nuevolibro);

    }
    [HttpPost]
    public IActionResult Editar(Libro libroActualizado)
    {
        if (ModelState.IsValid)
        {
            _context.Libros.Update(libroActualizado);
            _context.SaveChanges();
            return RedirectToAction("buscar", new { codlibro = libroActualizado.Codlibro });
        }
        return View(libroActualizado);
    }
    public IActionResult Eliminar(string codlibro)
    {
        var libro = _context.Libros.FirstOrDefault(l => l.Codlibro == codlibro);
        if (libro != null)
        {
            _context.Libros.Remove(libro);
            _context.SaveChanges();
        }
        return RedirectToAction("register"); 
    }
          
}