using System;

namespace Cotizaciones.Models
{
    public class DatosCotizacion
    {
        public Int64 Cotizacion { get; set; }
        public decimal Prima { get; set; }
        public decimal Premio { get; set; }
        public string Observaciones { get; set; }
        public string Mca_Carta_Oferta { get; set; }
        public Byte[] CartaOferta { get; set; }

    }
}
