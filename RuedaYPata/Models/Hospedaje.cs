using System.Collections.Generic;

namespace RuedaYPata.Models
{
    public class Hospedaje
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioPorDia { get; set; }

        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }

        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }

        // Navegación a Reservas y Comentarios
        public ICollection<ReservaHospedaje> Reservas { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}