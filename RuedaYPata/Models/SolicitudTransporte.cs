using System;

namespace RuedaYPata.Models
{
    public class SolicitudTransporte
    {
        public int Id { get; set; }

        public int TransporteId { get; set; }
        public Transporte Transporte { get; set; }

        public int MascotaId { get; set; }
        public Mascota Mascota { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
    }
}