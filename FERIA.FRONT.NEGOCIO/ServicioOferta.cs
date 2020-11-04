using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioOferta
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaOferta Leer(int id, string idSession)
        {
            RespuestaOferta respuesta = new RespuestaOferta();
            var respuestaApi = servicio.Get("api/Admin/Oferta/" + id, new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOfertaListar Listar(int IdProductor, string idSession)
        {
            RespuestaOfertaListar respuesta = new RespuestaOfertaListar();
            var respuestaApi = servicio.Get("api/Admin/Oferta/"+ IdProductor +"/Listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOfertaListar Listar(string idSession)
        {
            RespuestaOfertaListar respuesta = new RespuestaOfertaListar();
            var respuestaApi = servicio.Get("api/Admin/Oferta/Listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOferta Crear(Oferta oferta, string idSession)
        {
            RespuestaOferta respuesta = new RespuestaOferta();
            var respuestaApi = servicio.Post("api/Admin/Oferta", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            oferta);
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        
    }
}
