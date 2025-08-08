// Models/Ubicacion.cs
namespace RuedaYPata.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }  // Ej: Ciudad, Estado, etc.

        // Opcional: puedes tener relaciones si quieres
        // Por ejemplo, lista de hospedajes en esta ubicación
        public ICollection<Hospedaje> Hospedajes { get; set; }
    }
}