using FERIA.CLASES;
using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class ProductoController : ApiController
    {
        public RespuestaProducto Post([FromBody]Producto producto, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "No posee acceso valido", Producto = new Producto() };
            }
            ServicioProducto servicioProducto = new ServicioProducto(idSession);
            return servicioProducto.Crear(producto);

        }


        [HttpPost]
        [Route("api/producto/{IdProducto}/modificar")]
        public RespuestaProducto PostUpdate(int IdProducto, [FromBody] Producto producto, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "No posee acceso valido", Producto = new Producto() };
            }
            ServicioProducto servicioProducto = new ServicioProducto(idSession);
            return servicioProducto.Modificar(producto);

        }
        [HttpGet]        
        public RespuestaProducto Get(int id, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            { 
                return new RespuestaProducto() { Exito = false, Mensaje = "No posee acceso valido", Producto = new Producto() };
            }
            return new ServicioProducto().Leer(id);
        }
        [HttpGet]
        [Route("api/producto/{IdUsuario}/listar")]
        public RespuestaProductoListar GetListarByIdUsuario(int IdUsuario, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProductoListar() { Exito = false, Mensaje = "No posee acceso valido", Productos = new List<Producto>() };
            }
            return new ServicioProducto().Listar(IdUsuario);
        }
        [HttpGet]
        [Route("api/producto/listar")]
        public RespuestaProductoListar GetListar(string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProductoListar() { Exito = false, Mensaje = "No posee acceso valido", Productos = new List<Producto>() };
            }
            return new ServicioProducto().Listar(0);
        }

    }
}
