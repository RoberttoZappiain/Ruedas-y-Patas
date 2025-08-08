using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPata.Data;
using RuedaYPata.Models;
using RuedaYPata.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RuedaYPata.Controllers
{
    [Authorize]
    public class MascotasController : Controller
    {
        private readonly RuedaYPataContext _context;

        public MascotasController(RuedaYPataContext context)
        {
            _context = context;
        }

        // GET: Mascotas/Create
        public async Task<IActionResult> Create()
        {
            var tiposAnimales = new List<string> { "Perro", "Gato", "Otro" };
            var razas = await _context.Razas.ToListAsync();

            var model = new MascotaCreateViewModel
            {
                TiposAnimales = tiposAnimales.Select(t => new SelectListItem { Value = t, Text = t }).ToList(),
                Razas = razas.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Nombre }).ToList()
            };

            return View(model);
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MascotaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repoblar selects si el modelo es inválido
                var tiposAnimales = new List<string> { "Perro", "Gato", "Otro" };
                var razas = await _context.Razas.ToListAsync();

                model.TiposAnimales = tiposAnimales.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
                model.Razas = razas.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Nombre }).ToList();

                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mascota = new Mascota
            {
                Nombre = model.Nombre,
                Edad = model.Edad,
                TipoAnimal = model.TipoAnimal,
                RazaId = model.RazaId,
                UsuarioId = userId
            };

            _context.Add(mascota);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mascota creada correctamente.";
            return RedirectToAction(nameof(Create)); // O a donde quieras redirigir
        }
    }
}