using FERIA.CLASES;
using FERIA.NEGOCIO;
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
            ServicioOrden servicioOrden = new ServicioOrden(idSession);
            return servicioOrden.Crear(orden);
            
        }


        [HttpPost]
        [Route("api/orden/{IdOrden}/modificar")]
        public RespuestaOrden PostUpdate(int IdOrden, [FromBody]Orden orden, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "No posee acceso valido", Orden = new Orden() };
            }
            ServicioOrden servicioOrden = new ServicioOrden(idSession);
            return servicioOrden.Modificar(orden);
            
        }
        [HttpGet]
        [Route("api/orden/{IdUsuario}/Listar")]
        public RespuestaOrdenListar GetListar(int IdUsuario, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No posee acceso valido", Ordenes = new List<Orden>() };
            }
            ServicioOrden servicioOrden = new ServicioOrden(idSession);
            return servicioOrden.Listar(IdUsuario);

        }
        [HttpGet]
        [Route("api/orden/Listar")]
        public RespuestaOrdenListar GetListar(string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No posee acceso valido", Ordenes = new List<Orden>() };
            }
            ServicioOrden servicioOrden = new ServicioOrden(idSession);
            return servicioOrden.Listar();

        }
        [HttpGet]
        [Route("api/orden/{id}")]
        public RespuestaOrden Get(int id, string idSession="")
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "No posee acceso valido", Orden = new Orden() };
            }
            ServicioOrden servicioOrden = new ServicioOrden(idSession);
            return servicioOrden.Leer(id);

        }
    }
}
