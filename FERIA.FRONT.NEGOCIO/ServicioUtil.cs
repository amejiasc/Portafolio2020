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
    }
}
