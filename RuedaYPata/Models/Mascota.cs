using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPata.Models
{
    public class Mascota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Range(0, 50)]
        public int Edad { get; set; }

        [Required]
        [ForeignKey("Raza")]
        public int RazaId { get; set; }
        public Raza Raza { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        public string FotoRuta { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoAnimal { get; set; }

        // Relaciones con SolicitudesTransporte
        public ICollection<SolicitudTransporte> SolicitudesTransporte { get; set; }

        // Relaciones con ReservasHospedaje
        public ICollection<ReservaHospedaje> ReservasHospedaje { get; set; }
    }
}