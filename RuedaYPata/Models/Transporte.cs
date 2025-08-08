using System.Collections.Generic;

namespace RuedaYPata.Models
{
    public class Transporte
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }

        public ICollection<SolicitudTransporte> SolicitudesTransporte { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}