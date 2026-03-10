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
    [HttpGet]
    public IActionResult Login()
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
            
            return RedirectToAction("Login", "Usuario");
        }
        return View(nuevoUsuario);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string correo, string contraseña)
    {
        // Buscamos el usuario que coincida con ambos campos
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Correo == correo && u.Contraseña == contraseña);

        if (usuario != null)
        {
            // Creamos la sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.Idusuario);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

            return RedirectToAction("register", "Libro");
        }

        // Si falla, mandamos un mensaje de error a la vista
        ViewBag.Error = "Correo o contraseña incorrectos.";
        return View();
    }

    // --- LOGOUT ---
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}


