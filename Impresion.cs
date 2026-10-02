using Intranet.Models;
using Intranet.Models.Emision;
using Intranet.Utils;
using LibreriaDB;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Intranet.Conectores.Emision
{
    public class Impresion
    {
        public static Byte[] cartaOferta(string Cotizacion)
        {
            Byte[] pdf = null;
            string plantilla = "";
            DataTable datosCartaOferta = new DataTable();
            string nombrePlantilla = null;
            List<String> lista = new List<String>();

            ParametrosOracle[] parametros = new ParametrosOracle[6];
            parametros[0] = new ParametrosOracle("p_num_cotizacion", Cotizacion);
            parametros[1] = new ParametrosOracle("p_cod_usr", "USREMIS");
            parametros[2] = new ParametrosOracle("p_cod_agt", "99998");
            parametros[3] = new ParametrosOracle("p_mca_co_franquicia", "0");
            parametros[4] = new ParametrosOracle("p_nombre_plantilla", plantilla, false, true, false, 100);
            parametros[5] = new ParametrosOracle("o_cursor", "", true, false, false, 100);
            // bytes = Conexion.recuperaByte("em_k_cotiz_impresion_autos.p_carta_oferta", parametros, conexion);
            lista = Conexion.ejecutaProcedimiento("em_k_cotiz_impresion_autos.p_carta_oferta", parametros, Constantes.ConexionBaseDatos.Emision);
            //datosCartaOferta = Conexion.recuperaTablaOracle("em_k_cotiz_impresion_autos.p_carta_oferta", parametros, false, Constantes.ConexionBaseDatos.Emision);
            //

            ParametrosOracle[] parametrosco = new ParametrosOracle[5];
            parametrosco[0] = new ParametrosOracle("p_num_cotizacion", Cotizacion);
            parametrosco[1] = new ParametrosOracle("p_cod_usr", "USREMIS");
            parametrosco[2] = new ParametrosOracle("p_cod_agt", "99998");
            parametrosco[3] = new ParametrosOracle("p_mca_co_franquicia", "0");
            parametrosco[4] = new ParametrosOracle("p_nombre_plantilla", plantilla, false, true, false, 100);
            //parametros[5] = new ParametrosOracle("o_cursor", "", true, false, false, 100);
            // bytes = Conexion.recuperaByte("em_k_cotiz_impresion_autos.p_carta_oferta", parametros, conexion);
            //lista = Conexion.ejecutaProcedimiento("em_k_cotiz_impresion_autos.p_carta_oferta", parametros, Constantes.ConexionBaseDatos.Emision);
            datosCartaOferta = Conexion.recuperaTablaOracle("em_k_cotiz_impresion_autos.p_carta_oferta", parametrosco, false, Constantes.ConexionBaseDatos.Emision);   

            nombrePlantilla = lista[4];
            pdf = LibreriaDB.Conexion.impresion(Constantes.DirectorioPlantillasPDF + nombrePlantilla, datosCartaOferta);

            return pdf;
        }

    }
}