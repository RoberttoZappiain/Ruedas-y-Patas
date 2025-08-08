// Models/Comentario.cs
using System;

namespace RuedaYPata.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }

        public string Texto { get; set; }
        public DateTime Fecha { get; set; }

        public int? HospedajeId { get; set; }
        public Hospedaje Hospedaje { get; set; }

        public int? TransporteId { get; set; }
        public Transporte Transporte { get; set; }
    }
}