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
            List<CLASES.Region> regiones = new List<CLASES.Region>();
            if (HttpRuntime.Cache.Get("Regiones") == null)
            {
                regiones = ServicioUtil.Regiones();
                HttpRuntime.Cache.Insert("Regiones", regiones, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                regiones = (List<CLASES.Region>)HttpRuntime.Cache.Get("Regiones");
            }
            return regiones;
        }
        public static List<CLASES.Categoria> ListarCategorias()
        {
            ServicioUtil ServicioUtil = new ServicioUtil();
            List<CLASES.Categoria> Categorias = new List<CLASES.Categoria>();
            if (HttpRuntime.Cache.Get("Categorias") == null)
            {
                Categorias = ServicioUtil.Categorias();
                HttpRuntime.Cache.Insert("Categorias", Categorias, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                Categorias = (List<CLASES.Categoria>)HttpRuntime.Cache.Get("Categorias");
            }
            return Categorias;
        }
        public static void Destruir(string Objeto)
        {
            if (HttpRuntime.Cache.Get(Objeto) != null)
            {
                HttpRuntime.Cache.Remove(Objeto);
            }
        }
        public static List<CLASES.Perfil> ListarPerfiles()
        {
            ServicioUtil ServicioUtil = new ServicioUtil();
            List<CLASES.Perfil> Perfiles = new List<CLASES.Perfil>();
            if (HttpRuntime.Cache.Get("Perfiles") == null)
            {
                Perfiles = ServicioUtil.Perfiles();
                HttpRuntime.Cache.Insert("Perfiles", Perfiles, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
            }
            else
            {
                Perfiles = (List<CLASES.Perfil>)HttpRuntime.Cache.Get("Perfiles");
            }
            return Perfiles;
        }
    }
}