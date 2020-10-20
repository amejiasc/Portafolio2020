using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioProducto
    {
        ServicioApi servicio = new ServicioApi();

        public RespuestaProducto Leer(int idProducto, string idSession)
        {
            RespuestaProducto respuesta = new RespuestaProducto();
            var respuestaApi = servicio.Get("api/producto/" + idProducto, new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaProductoListar Listar(int IdUsuario, string idSession)
        {
            RespuestaProductoListar respuesta = new RespuestaProductoListar();
            var respuestaApi = servicio.Get("api/producto/" + IdUsuario + "/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaProductoListar Listar(string idSession)
        {
            RespuestaProductoListar respuesta = new RespuestaProductoListar();
            var respuestaApi = servicio.Get("api/producto/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaProducto Crear(Producto producto, string idSession)
        {
            RespuestaProducto respuesta = new RespuestaProducto();
            var respuestaApi = servicio.Post("api/producto", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            producto);
            if (respuestaApi.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "No hay acceso a la api o no existe" };
            }
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;

        }
        public RespuestaProducto Modificar(Producto producto, string idSession)
        {
            RespuestaProducto respuesta = new RespuestaProducto();
            var respuestaApi = servicio.Post("api/producto/" + producto.IdProducto + "/modificar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            producto);
            if (respuestaApi.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "No hay acceso a la api o no existe" };
            }
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
    }
}
