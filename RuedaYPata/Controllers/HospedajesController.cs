using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPata.Data;
using RuedaYPata.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RuedaYPata.Controllers
{
    public class HospedajesController : Controller
    {
        private readonly RuedaYPataContext _context;

        public HospedajesController(RuedaYPataContext context)
        {
            _context = context;
        }

        // GET: Hospedajes
        public async Task<IActionResult> Index()
        {
            var hospedajes = _context.Hospedajes.Include(h => h.Ubicacion);
            return View(await hospedajes.ToListAsync());
        }

        // GET: Hospedajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hospedaje = await _context.Hospedajes
                .Include(h => h.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hospedaje == null) return NotFound();

            return View(hospedaje);
        }

        // GET: Hospedajes/Create
        public async Task<IActionResult> Create()
        {
            var ubicaciones = await _context.Ubicaciones.ToListAsync();
            ViewBag.UbicacionId = new SelectList(ubicaciones, "Id", "Nombre");
            return View();
        }

        // POST: Hospedajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hospedaje hospedaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospedaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var ubicaciones = await _context.Ubicaciones.ToListAsync();
            ViewBag.UbicacionId = new SelectList(ubicaciones, "Id", "Nombre", hospedaje.UbicacionId);
            return View(hospedaje);
        }

        // GET: Hospedajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hospedaje = await _context.Hospedajes.FindAsync(id);
            if (hospedaje == null) return NotFound();

            var ubicaciones = await _context.Ubicaciones.ToListAsync();
            ViewBag.UbicacionId = new SelectList(ubicaciones, "Id", "Nombre", hospedaje.UbicacionId);

            return View(hospedaje);
        }

        // POST: Hospedajes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hospedaje hospedaje)
        {
            if (id != hospedaje.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospedaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospedajeExists(hospedaje.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var ubicaciones = await _context.Ubicaciones.ToListAsync();
            ViewBag.UbicacionId = new SelectList(ubicaciones, "Id", "Nombre", hospedaje.UbicacionId);
            return View(hospedaje);
        }

        // GET: Hospedajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hospedaje = await _context.Hospedajes
                .Include(h => h.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hospedaje == null) return NotFound();

            return View(hospedaje);
        }

        // POST: Hospedajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospedaje = await _context.Hospedajes.FindAsync(id);
            if (hospedaje != null)
            {
                _context.Hospedajes.Remove(hospedaje);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool HospedajeExists(int id)
        {
            return _context.Hospedajes.Any(e => e.Id == id);
        }
    }
}