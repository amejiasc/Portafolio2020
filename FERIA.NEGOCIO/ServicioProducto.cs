using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public  class ServicioProducto
    {
        STORE.ServicioProducto servicioProducto;
        ServicioCorreo servicioCorreo;


        public ServicioProducto(string IdSession = "")
        {
            this.servicioProducto = new STORE.ServicioProducto(IdSession);
            this.servicioCorreo = new ServicioCorreo();

        }

        public RespuestaProductoListar Listar(int idProductor)
        {
            List<Producto> listados;
            if (idProductor == 0) {
                listados = servicioProducto.Listar();
            }
            else
                listados = servicioProducto.Listar(idProductor);

            if (listados == null)
            {
                return new RespuestaProductoListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los productos" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaProductoListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaProductoListar() { Exito = true, Productos = listados, Mensaje = "" };

        }
        public RespuestaProducto Leer(int idProducto)
        {
            var producto = servicioProducto.Leer(idProducto);

            if (producto == null)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los productos" };
            }
            if (producto.IdProducto == 0)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaProducto() { Exito = true, Producto = producto, Mensaje = "" };

        }

        public RespuestaProducto Crear(Producto producto)
        {

            var listado = Listar(producto.IdProducto);
            if (listado.Productos.Exists(x => x.CodigoProducto.Equals(producto.CodigoProducto))) {
                return new RespuestaProducto() { Exito = false, Mensaje = "Producto ya existe ingresado" };
            }
            var respuesta = servicioProducto.Crear(producto);
            if (respuesta == null)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar el producto" };
            }
            if (respuesta.IdProducto.Equals(0))
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Producto no fue creado" };
            }
            return new RespuestaProducto() { Exito = true, Mensaje = "Creación Exitosa", Producto = respuesta };
        }
        public RespuestaProducto Modificar(Producto producto)
        {
            var listado = Listar(producto.IdProducto);
            if (listado.Productos.Exists(x => !x.IdProducto.Equals(producto.IdProducto) &&  x.CodigoProducto.Equals(producto.CodigoProducto)))
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Producto ya existe ingresado" };
            }
            var respuesta = servicioProducto.Modificar(producto);
            if (respuesta == null)
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de modificar el producto" };
            }
            if (respuesta.IdProducto.Equals(0))
            {
                return new RespuestaProducto() { Exito = false, Mensaje = "Producto no fue posible modificar" };
            }            
            return new RespuestaProducto() { Exito = true, Mensaje = "Modificación Exitosa", Producto = respuesta };
        }

    }
}
