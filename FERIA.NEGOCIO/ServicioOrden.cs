using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioOrden
    {
        STORE.ServicioOrden servicioOrden;
        STORE.ServicioUsuario servicioUsuario;
        public ServicioOrden(string IdSession = "")
        {
            this.servicioOrden = new STORE.ServicioOrden(IdSession);
            this.servicioUsuario = new STORE.ServicioUsuario(IdSession);
        }
        public RespuestaFirmaOrden Firmar(int idOrden) {
            if (idOrden == 0) {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "Id Orden no puede ser vacío o cero" };
            }
            var orden = servicioOrden.Leer(idOrden);
            if (orden == null)
            {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "No se encontró la orden" };
            }
            if (orden.Estado != "PENDIENTE")
            {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "La orden ya ha sido firmada." };
            }
            var id = servicioOrden.Firmar(idOrden);
            if (id == 0)
            {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "No fue posible firmar la orden" };
            }
            return new RespuestaFirmaOrden() { Exito = true, Mensaje = "" };
        }
        public RespuestaOrden Crear(Orden orden)
        {
            var respuesta = servicioOrden.Crear(orden);
            if (respuesta == null)
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar la Orden" };
            }
            if (respuesta.IdOrden.Equals(0))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "Orden no fue posible crearla" };
            }

            foreach (DetalleOrden item in orden.DetalleOrden)
            {
                var detalle = servicioOrden.CrearDetalle(orden.IdOrden, item);
                if (detalle.Equals(0))
                {
                    servicioOrden.EliminarDetalle(orden.IdOrden);
                    servicioOrden.Eliminar(orden.IdOrden);
                    return new RespuestaOrden() { Exito = false, Mensaje = "No fue posible crear los detalles, Orden no ha sido creada" };
                }
            }
            return new RespuestaOrden() { Exito = true, Mensaje = "Creación Exitosa", Orden = respuesta };

        }
        public RespuestaOrden Modificar(Orden orden)
        {
            var respuesta = servicioOrden.Modificar(orden);
            if (respuesta == null)
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de modificar la Orden" };
            }
            if (respuesta.IdOrden.Equals(0))
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "Orden no fue posible modificar" };
            }
            servicioOrden.EliminarDetalle(orden.IdOrden);
            foreach (DetalleOrden item in orden.DetalleOrden)
            {
                var detalle = servicioOrden.CrearDetalle(orden.IdOrden, item);
                if (detalle.Equals(0))
                {
                    return new RespuestaOrden() { Exito = false, Mensaje = "No fue posible crear los detalles" };
                }
            }
            return new RespuestaOrden() { Exito = true, Mensaje = "Modificación Exitosa", Orden = respuesta };

        }

        public RespuestaOrdenListar Listar(int IdUsuario)
        {
            var listado = servicioOrden.Listar().Where(x => x.IdClienteExterno.Equals(IdUsuario) || x.IdClienteInterno.Equals(IdUsuario)).ToList();
            if (listado == null)
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No se encontrarón datos", Ordenes = new List<Orden>() };
            }
            if (listado.Count < 1)
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No se encontrarón datos", Ordenes = new List<Orden>() };
            }
            return new RespuestaOrdenListar() { Exito = true, Mensaje = "", Ordenes = listado };
        }
        public RespuestaOrdenListar Listar()
        {
            var listado = servicioOrden.Listar();
            if (listado == null)
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No se encontrarón datos", Ordenes = new List<Orden>() };
            }
            if (listado.Count < 1)
            {
                return new RespuestaOrdenListar() { Exito = false, Mensaje = "No se encontrarón datos", Ordenes = new List<Orden>() };
            }
            return new RespuestaOrdenListar() { Exito = true, Mensaje = "", Ordenes = listado };
        }
        public RespuestaOrden Leer(int id)
        {
            var orden = servicioOrden.Leer(id);
            if (orden == null)
            {
                return new RespuestaOrden() { Exito = false, Mensaje = "No se encontrarón datos" };
            }
           
            return new RespuestaOrden() { Exito = true, Mensaje = "", Orden = orden };
        }

    }
}
