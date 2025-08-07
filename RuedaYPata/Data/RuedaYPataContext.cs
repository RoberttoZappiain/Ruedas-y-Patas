using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RuedaYPata.Models;

namespace RuedaYPata.Data
{
    public class RuedaYPataContext : IdentityDbContext<ApplicationUser>
    {
        public RuedaYPataContext(DbContextOptions<RuedaYPataContext> options)
            : base(options)
        {
        }

        // Catálogos
        // public DbSet<Raza> Razas { get; set; }
        // public DbSet<Ubicacion> Ubicaciones { get; set; }

        // // Entidades principales
        // public DbSet<Mascota> Mascotas { get; set; }
        // public DbSet<Hospedaje> Hospedajes { get; set; }
        // public DbSet<Transporte> Transportes { get; set; }
        // public DbSet<ReservaHospedaje> ReservasHospedaje { get; set; }
        // public DbSet<SolicitudTransporte> SolicitudesTransporte { get; set; }
        // public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Aquí podrías configurar restricciones, claves compuestas, etc.
        }
    }
}