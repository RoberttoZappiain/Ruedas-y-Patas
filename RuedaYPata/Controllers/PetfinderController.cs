using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuedaYPata.Data;
using RuedaYPata.Models;
using RuedaYPata.Services;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/petfinder")]
public class PetfinderApiController : ControllerBase
{
    private readonly RuedaYPataContext _context;
    private readonly PetfinderService _petfinderService;

    public PetfinderApiController(RuedaYPataContext context, PetfinderService petfinderService)
    {
        _context = context;
        _petfinderService = petfinderService;
    }

    // GET: /api/petfinder/razas/{tipo}
    [HttpGet("razas/{tipo}")]
    public async Task<IActionResult> GetRazas(string tipo)
    {
        // 1) Intentar usar razas existentes en BD
        var razasDb = await _context.Razas
                           .OrderBy(r => r.Nombre)
                           .Select(r => new { id = r.Id, nombre = r.Nombre })
                           .ToListAsync();

        if (razasDb.Any())
            return Ok(razasDb);

        // 2) Si BD vacía, intentar recuperar desde PetfinderService (retorna List<string> o List<RazaDto>)
        try
        {
            var respuesta = await _petfinderService.GetRazasAsync(tipo); // puede devolver List<string> o List<RazaDto>

            if (respuesta == null)
                return Ok(new object[0]);

            // Si GetRazasAsync devuelve List<string>
            if (respuesta is System.Collections.IEnumerable && !(respuesta is System.Collections.Generic.IEnumerable<object>)) { }

            // Manejo flexible: normalizar a lista de nombres
            var nombres = (respuesta as System.Collections.IEnumerable)
                          .Cast<object>()
                          .Select(o =>
                          {
                              if (o == null) return null;
                              // si es string
                              if (o is string s) return s;
                              // si es objeto con propiedades Nombre/Name
                              var propNombre = o.GetType().GetProperty("Nombre") ?? o.GetType().GetProperty("nombre") ?? o.GetType().GetProperty("Name");
                              return propNombre != null ? propNombre.GetValue(o)?.ToString() : o.ToString();
                          })
                          .Where(x => !string.IsNullOrWhiteSpace(x))
                          .Distinct()
                          .ToList();

            // Guardar en BD si no existen
            foreach (var nombre in nombres)
            {
                if (!await _context.Razas.AnyAsync(r => r.Nombre == nombre))
                {
                    _context.Razas.Add(new Raza { Nombre = nombre });
                }
            }

            await _context.SaveChangesAsync();

            // Devolver las razas guardadas
            var result = await _context.Razas
                .OrderBy(r => r.Nombre)
                .Select(r => new { id = r.Id, nombre = r.Nombre })
                .ToListAsync();

            return Ok(result);
        }
        catch
        {
            // Si falla la API externa devolvemos lo que tengamos en BD (posiblemente vacío)
            var fallback = await _context.Razas
                .OrderBy(r => r.Nombre)
                .Select(r => new { id = r.Id, nombre = r.Nombre })
                .ToListAsync();

            return Ok(fallback);
        }
    }
}