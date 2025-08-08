// Models/Raza.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPata.Models
{
    public class Raza
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la raza es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]

        public string Nombre { get; set; }
        
        public ICollection<Mascota> Mascotas { get; set; }
    }
}