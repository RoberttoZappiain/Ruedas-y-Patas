using System;

namespace RuedaYPata.Models
{
    public class ReservaHospedaje
    {
        public int Id { get; set; }

        public int HospedajeId { get; set; }
        public Hospedaje Hospedaje { get; set; }

        public int MascotaId { get; set; }
        public Mascota Mascota { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}