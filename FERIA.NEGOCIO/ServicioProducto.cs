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
            var listados = servicioProducto.Listar(idProductor);

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

    }
}
