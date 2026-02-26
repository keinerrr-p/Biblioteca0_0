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
            return View();
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
        return View(nuevolibro);
    }
        


    
        
    


    
}
