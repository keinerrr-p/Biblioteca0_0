using Microsoft.AspNetCore.Mvc;
using Biblioteca0_0.Models;
using Biblioteca0_0.Data;

namespace Biblioteca0_0.Controllers;

    public class UsuarioController : Controller
{
    public readonly Biblioteca01Context _context;

    public  UsuarioController(Biblioteca01Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }
    public IActionResult Users(string correo)
    {
        var users = _context.Usuarios.FirstOrDefault(u => u.Correo == correo );
         
        if (users == null)  
            return View("Users");

        return View(users);
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Registro(Usuario nuevoUsuario)
{
    if (ModelState.IsValid)
    {
        
        bool existeCorreo = _context.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo);
        
     
        bool existeDocumento = _context.Usuarios.Any(u => u.NumDoc == nuevoUsuario.NumDoc);

        if (existeCorreo)
        {
            
            ModelState.AddModelError("Correo", "Este correo electrónico ya está registrado.");
        }

        if (existeDocumento)
        {
            ModelState.AddModelError("NumDoc", "Este número de documento ya existe.");
        }

        
        if (existeCorreo || existeDocumento)
        {
            return View(nuevoUsuario);
        }

        
        _context.Usuarios.Add(nuevoUsuario);
        _context.SaveChanges();
        
        return RedirectToAction("Index", "Home");
    }

    return View(nuevoUsuario);
}
}


