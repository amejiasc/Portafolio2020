using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace FERIA.FRONT
{
    public class UtilConfig
    {
        static ServicioUtil ServicioUtil = new ServicioUtil();
        public static List<Region> Regiones()
        {

            List<Region> regiones = new List<Region>();
            if (HttpRuntime.Cache.Get("regiones") == null)
            {
                regiones = ServicioUtil.Regiones();
                HttpRuntime.Cache.Insert("regiones", regiones, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                regiones = (List<Region>)HttpRuntime.Cache.Get("regiones");
            }

            return regiones;


        }
        public static List<Comuna> Comunas()
        {

            List<Comuna> comunas = new List<Comuna>();
            if (HttpRuntime.Cache.Get("comunas") == null)
            {
                comunas = ServicioUtil.Comunas();
                HttpRuntime.Cache.Insert("comunas", comunas, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                comunas = (List<Comuna>)HttpRuntime.Cache.Get("comunas");
            }

            return comunas;


        }
    }
}