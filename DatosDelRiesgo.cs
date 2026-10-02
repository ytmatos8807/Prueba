using System.ComponentModel.DataAnnotations;

namespace Cotizaciones.Models
{
    public class DatosDelRiesgo
    {
        public string CodigoAcuerdo { get; set; }
        public string CodigoModalidad { get; set; }
        public string CodigoMoneda { get; set; }
        public string CodigoFraccionamiento { get; set; }
        public string ImporteInicial { get; set; }
//        public string CodigoUsuario { get; set; }
      //public Boolean CuotaIgual { get; set; }
        public bool MarcaReferido { get; set; }
//        public string CodigoAgente { get; set; }
        public string CodigoMarca { get; set; }
        public string CodigoModelo { get; set; }
        public string CodigoSubModelo { get; set; }
        public string Anio { get; set; }
        public string ImporteCapital { get; set; }
        public string Marca0Km { get; set; }
        public string MarcaIva { get; set; }
        //public string Codigonivel_bonus_malus { get; set; }
        public string CodigoTipoVehiculo { get; set; }
        public string NumeroOcupantes { get; set; }
        public string CodigoFranquicia { get; set; }
        public string NumeroCotizacion { get; set; }

        public string Error { get; set; }

        public int TipResultado { get; set; }
        public string NomError { get; set; }
        
    }
}
