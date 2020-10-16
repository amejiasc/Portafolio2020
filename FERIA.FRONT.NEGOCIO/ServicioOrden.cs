using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioOrden
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaOrden Leer(int id, string idSession)
        {
            RespuestaOrden respuesta = new RespuestaOrden();
            var respuestaApi = servicio.Get("api/orden/" + id, new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOrdenListar Listar(int IdUsuario, string idSession)
        {
            RespuestaOrdenListar respuesta = new RespuestaOrdenListar();
            var respuestaApi = servicio.Get("api/orden/" + IdUsuario + "/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOrdenListar Listar(string idSession)
        {
            RespuestaOrdenListar respuesta = new RespuestaOrdenListar();
            var respuestaApi = servicio.Get("api/orden/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOrden Crear(Orden orden, string idSession)
        {
            RespuestaOrden respuesta = new RespuestaOrden();
            var respuestaApi = servicio.Post("api/orden", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            orden);
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOrden Modificar(Orden orden, string idSession)
        {
            RespuestaOrden respuesta = new RespuestaOrden();
            var respuestaApi = servicio.Post("api/orden/"+ orden.IdOrden +"/modificar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            orden);
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
    }
}
