using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPata.ViewModels
{
    public class MascotaCreateViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Range(0, 50)]
        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Required]
        [Display(Name = "Tipo de Animal")]
        public string TipoAnimal { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public int RazaId { get; set; }

        public IEnumerable<SelectListItem> TiposAnimales { get; set; }
        public IEnumerable<SelectListItem> Razas { get; set; }
    }
}