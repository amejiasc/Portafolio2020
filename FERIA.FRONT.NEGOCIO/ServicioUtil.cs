using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioUtil
    {
        ServicioApi servicio = new ServicioApi();
        public List<Region> Regiones()
        {
            List<Region> regiones = new List<Region>();
            var respuesta = servicio.Get("api/region", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, regiones);
            return msj;
        }
        public List<Comuna> Comunas()
        {
            List<Comuna> comunas = new List<Comuna>();
            var respuesta = servicio.Get("api/comuna", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, comunas);
            return msj;
        }
        public List<string> EstadoOrden()
        {
            List<string> estados = new List<string>();
            var respuesta = servicio.Get("api/util/Estado/Orden", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, estados);
            return msj;
        }
        public List<string> EstadoProceso()
        {
            List<string> estados = new List<string>();
            var respuesta = servicio.Get("api/util/Estado/Proceso", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, estados);
            return msj;
        }
        public List<string> EstadoOferta()
        {
            List<string> estados = new List<string>();
            var respuesta = servicio.Get("api/util/Estado/Oferta", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, estados);
            return msj;
        }
        public List<Categoria> Categorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            var respuesta = servicio.Get("api/util/categorias", new List<RestSharp.Parameter>());
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, categorias);
            return msj;
        }
    }
}
