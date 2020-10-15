using FERIA.CLASES;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class OrdenController : ApiController
    {
        // POST: api/Usuario
        public RespuestaOrden Post([FromBody]Orden orden, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "No posee acceso valido", Orden = new Orden() };
            }
            //ServicioOrden servicioOrden = new ServicioOrden(idSession);
            //return JObject.FromObject(servicioOrden.Crear(orden));
            return new RespuestaOrden() { };

        }


        [HttpPost]
        [Route("api/orden/{id}/modificar")]
        public RespuestaOrden PostUpdate(int id,[FromBody]Orden orden, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "No posee acceso valido", Orden = new Orden() };
            }
            //ServicioOrden servicioOrden = new ServicioOrden(idSession);
            //return JObject.FromObject(servicioOrden.Modificar(orden));
            return new RespuestaOrden() { };
        }
    }
}
