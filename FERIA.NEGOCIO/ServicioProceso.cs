using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioProceso
    {
        STORE.ServicioProceso servicioProceso;
        public ServicioProceso(string IdSession = "")
        {
            this.servicioProceso = new STORE.ServicioProceso(IdSession);
        }
        public RespuestaProceso Crear(Proceso proceso)
        {

            //var listado = Listar(producto.IdProducto);
            //if (listado.Productos.Exists(x => x.CodigoProducto.Equals(producto.CodigoProducto)))
            //{
            //    return new RespuestaProducto() { Exito = false, Mensaje = "Código de Producto ya existe ingresado" };
            //}
            //var respuesta = servicioProducto.Crear(producto);
            //if (respuesta == null)
            //{
            //    return new RespuestaProducto() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar el producto" };
            //}
            //if (respuesta.IdProducto.Equals(0))
            //{
            //    return new RespuestaProducto() { Exito = false, Mensaje = "Producto no fue creado" };
            //}
            return new RespuestaProceso() { Exito = true, Mensaje = "Creación Exitosa", Proceso = proceso };
        }

    }
}
