// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace RuedaYPata.Models // ajusta este namespace al de tu proyecto
{
    public class ApplicationUser : IdentityUser
    {
        // Campos adicionales
        public string NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}