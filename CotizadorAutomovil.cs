using Intranet.Conectores.Cobranzas;
using Intranet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using Cotizaciones.Models;
using LibreriaDB;
using System.Data;
using Intranet.Conectores.Emision;


namespace Intranet.Controllers.CotizadorAutomovil
{
    [RoutePrefix("api/CotizadorAutomovil")]
    public class CotizadorAutomovilController : ApiController
    {
        [Route("GenerarCotizacion")]
        // GET: CotizadorAutomovil
        public ResultadoCotizacion CostosAutos([FromBody] DatosDelRiesgo datosRiesgo)
        {
            //DataTable dt = new DataTable();
            //dt = this.ObtenerResultadosDesdeBD(datosRiesgoRiesgo);
            //var resultado = new List<ResultadoCotizacion>();
            //  resultado = ConvertirDataTableALista(dt);
            //{

            var resultado = new ResultadoCotizacion
            {
                Resultado = true,
                Observaciones = "",
                Cotizaciones = new List<DatosCotizacion>() // Inicialización de la lista
            };

            DataTable resultadoOracle = new DataTable();
            ParametrosOracle[] parametros = new ParametrosOracle[26];

            parametros[0] = new ParametrosOracle("p_cod_acuerdo", datosRiesgo.CodigoAcuerdo);
            parametros[1] = new ParametrosOracle("p_cod_modalidad", datosRiesgo.CodigoModalidad);
            parametros[2] = new ParametrosOracle("p_cod_mon", datosRiesgo.CodigoMoneda);
            parametros[3] = new ParametrosOracle("p_cod_fracc_pago", datosRiesgo.CodigoFraccionamiento);
            parametros[4] = new ParametrosOracle("p_imp_inicial", datosRiesgo.ImporteInicial);
            parametros[5] = new ParametrosOracle("p_cod_usr", "USREMIS");
            parametros[6] = new ParametrosOracle("p_mca_cuota_igual", "S");
            parametros[7] = new ParametrosOracle("p_mca_referido", "N");
            parametros[8] = new ParametrosOracle("p_cod_agt", "99998");
            parametros[9] = new ParametrosOracle("p_val_cambio", "1");
            parametros[10] = new ParametrosOracle("p_mca_canal", "N");
            parametros[11] = new ParametrosOracle("p_cod_marca", datosRiesgo.CodigoMarca.ToString());
            parametros[12] = new ParametrosOracle("p_cod_modelo", datosRiesgo.CodigoModelo.ToString());
            parametros[13] = new ParametrosOracle("p_cod_sub_modelo", datosRiesgo.CodigoSubModelo.ToString());
            parametros[14] = new ParametrosOracle("p_anio_sub_modelo", datosRiesgo.Anio);
            parametros[15] = new ParametrosOracle("p_val_sub_modelo", datosRiesgo.ImporteCapital);
            parametros[16] = new ParametrosOracle("p_mca_0_km", datosRiesgo.Marca0Km.ToString(),true);
            parametros[17] = new ParametrosOracle("p_mca_iva", datosRiesgo.MarcaIva.ToString(),true);
            parametros[18] = new ParametrosOracle("p_cod_nivel_bonus_malus", null);
            parametros[19] = new ParametrosOracle("p_cod_tip_vehi", datosRiesgo.CodigoTipoVehiculo);
            parametros[20] = new ParametrosOracle("p_num_plazas", datosRiesgo.NumeroOcupantes);
            parametros[21] = new ParametrosOracle("p_val_franquicia", datosRiesgo.CodigoFranquicia);
            parametros[22] = new ParametrosOracle("p_num_cotizacion", datosRiesgo.NumeroCotizacion);
            parametros[23] = new ParametrosOracle("p_error", datosRiesgo.Error,false,true,false,100);
            parametros[24] = new ParametrosOracle("p_tip_resultado", datosRiesgo.TipResultado);
            parametros[25] = new ParametrosOracle("p_nom_error", datosRiesgo.NomError, false, true, false, 100);
            //parametros[26] = new ParametrosOracle("o_cursor", null, true, false, false, 0);

            resultadoOracle = Conexion.recuperaTablaOracle("EM_K_API_COTIZ.p_devuelve_costos", parametros, false, Constantes.ConexionBaseDatos.Emision);


            foreach (DataRow row in resultadoOracle.Rows)
            {
                var cotizacion = new DatosCotizacion
                {
                    Cotizacion = Convert.ToInt64(row["NUM_COTIZACION"]),
                    Prima = row.Field<decimal>("IMP_PRIMA"),
                    Premio = row.Field<decimal>("IMP_CUOTA"),
                    Observaciones = row.Field<string>("NOM_DESCRIPCION"),
                   // CartaOferta = Impresion.cartaOferta(row["NUM_COTIZACION"].ToString()),
                    Mca_Carta_Oferta = row.Field<string>("MCA_CARTA_OFERTA")
                   
                    //CartaOferta = Convert.ToInt32(row["NOMBRE_COLUMNA"])
                    //CartaOferta = new Byte[10]
                };
                if (cotizacion.Mca_Carta_Oferta == "S")
                {
                    cotizacion.CartaOferta = Impresion.cartaOferta(cotizacion.Cotizacion.ToString());
                }
                resultado.Cotizaciones.Add(cotizacion);
            }

            return resultado;

        }
    }
}