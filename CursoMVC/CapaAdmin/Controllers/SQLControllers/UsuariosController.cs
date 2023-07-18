using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaAdmin.Models;


namespace CapaAdmin.Controllers.SQLControllers
{
    public class UsuariosController : Controller
    {
        private readonly DbcarritoContext _context;

        public UsuariosController(DbcarritoContext context)
        {
            _context = context;
        }
        private Usuario ObtenerUsuarioActual()
        {
            // Aquí debes implementar tu propia lógica para obtener el usuario actual
            // Puedes utilizar el sistema de autenticación de ASP.NET, como HttpContext.User, para obtener la información del usuario actualmente autenticado
            // Por ejemplo:
            string userName = HttpContext.User.Identity.Name; // Obtener el nombre del usuario autenticado

            // Buscar el usuario en la base de datos utilizando el nombre
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombres == userName);

            return usuario;
        }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            ViewBag.EsNuevo = true;
            IEnumerable<Usuario> usuarios = await _context.Usuarios.ToListAsync();

            // Obtener el usuario actual desde tu lógica de negocio
            Usuario usuarioActual = ObtenerUsuarioActual();

            // Crear una instancia de UsuarioViewModel y asignar los valores
            UsuarioViewModel viewModel = new UsuarioViewModel
            {
                Usuarios = usuarios,
                Usuario = usuarioActual
            };
            return _context.Usuarios != null ? 
                          View(viewModel) :
                          Problem("Entity set 'DbcarritoContext.Usuarios'  is null.");
        }




        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombres,Apellidos,Correo,Clave,Reestablecer,Activo,FechaRegistro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpGet("getUsuario/{id}")]
        public async Task<Usuario>Get(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombres,Apellidos,Correo,Clave,Reestablecer,Activo,FechaRegistro")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DbcarritoContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
