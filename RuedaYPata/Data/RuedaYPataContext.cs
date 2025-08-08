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

        public DbSet<Raza> Razas { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Hospedaje> Hospedajes { get; set; }
        public DbSet<Transporte> Transportes { get; set; }
        public DbSet<ReservaHospedaje> Reservas { get; set; }
        public DbSet<SolicitudTransporte> SolicitudesTransporte { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar DeleteBehavior.Restrict para evitar cascadas múltiples

            modelBuilder.Entity<SolicitudTransporte>(entity =>
            {
                entity.HasOne(st => st.Transporte)
                      .WithMany(t => t.SolicitudesTransporte)
                      .HasForeignKey(st => st.TransporteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(st => st.Mascota)
                      .WithMany(m => m.SolicitudesTransporte)
                      .HasForeignKey(st => st.MascotaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReservaHospedaje>(entity =>
            {
          
                entity.HasOne(r => r.Hospedaje)
                      .WithMany(h => h.Reservas)
                      .HasForeignKey(r => r.HospedajeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar precisión decimal para evitar warnings
            modelBuilder.Entity<Hospedaje>()
                .Property(h => h.PrecioPorDia)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transporte>()
                .Property(t => t.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }
}