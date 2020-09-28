using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace FERIA.API.App_Start
{
    public class UtilConfig
    {
        
        public static  List<CLASES.Comuna> ListarComunas() 
        {
            ServicioUtil ServicioUtil = new ServicioUtil();
            List<CLASES.Comuna> Comunas = new List<CLASES.Comuna>();
            if (HttpRuntime.Cache.Get("Comunas") == null)
            {
                Comunas = ServicioUtil.Comunas();
                HttpRuntime.Cache.Insert("Comunas", Comunas, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                Comunas = (List<CLASES.Comuna>)HttpRuntime.Cache.Get("Comunas");
            }
            return Comunas;
        }
        public static List<CLASES.Region> ListarRegiones()
        {
            ServicioUtil ServicioUtil = new ServicioUtil();
            List<CLASES.Region> Comunas = new List<CLASES.Region>();
            if (HttpRuntime.Cache.Get("Regiones") == null)
            {
                Comunas = ServicioUtil.Regiones();
                HttpRuntime.Cache.Insert("Regiones", Comunas, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                Comunas = (List<CLASES.Region>)HttpRuntime.Cache.Get("Regiones");
            }
            return Comunas;
        }
    }
}