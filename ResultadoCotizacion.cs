using System.Collections.Generic;
namespace Cotizaciones.Models
{
    public class ResultadoCotizacion
    {
        public bool Resultado { get; set; }
        public string Observaciones { get; set; }
        public List<DatosCotizacion> Cotizaciones { get; set; }
    }
}
