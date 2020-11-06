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
        STORE.ServicioOrden servicioOrden;
        STORE.ServicioOferta servicioOferta;
        STORE.ServicioUsuario servicioUsuario;
        ServicioCorreo servicioCorreo = new ServicioCorreo();
        public ServicioProceso(string IdSession = "")
        {
            this.servicioProceso = new STORE.ServicioProceso(IdSession);
            this.servicioOrden = new STORE.ServicioOrden(IdSession);
            this.servicioOferta = new STORE.ServicioOferta(IdSession);
            this.servicioUsuario = new STORE.ServicioUsuario(IdSession);
        }
        public RespuestaProceso Modificar(Proceso proceso)
        {

            var listado = Listar();
            if (listado.Procesos.Exists(x => !x.IdProceso.Equals(proceso.IdProceso) && !x.EstadoProceso.Equals("ANULADO") &&  x.IdOrden.Equals(proceso.IdOrden) ))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Código de Orden ya existe en un proceso" };
            }
            var respuesta = servicioProceso.Modificar(proceso);
            if (respuesta == null)
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de modificar el proceso" };
            }
            if (respuesta.IdProceso.Equals(0))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Proceso no fue modificado" };
            }
            return new RespuestaProceso() { Exito = true, Mensaje = "Modificación Exitosa", Proceso = proceso };
        }
        public RespuestaProceso Crear(Proceso proceso)
        {

            var orden = servicioOrden.Leer(proceso.IdOrden);
            if (orden == null)
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "No se encontró la orden" };
            }
            if (orden.Estado == "PENDIENTE")
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "La orden no se ha firmado aún." };
            }


            var listado = Listar();
            if (listado.Procesos.Exists(x => !x.EstadoProceso.Equals("ANULADO") && x.IdOrden.Equals(proceso.IdOrden)))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Código de Orden ya existe en un proceso" };
            }
            var respuesta = servicioProceso.Crear(proceso);
            if (respuesta == null)
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar el proceso" };
            }
            if (respuesta.IdProceso.Equals(0))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Proceso no fue creado" };
            }
            var productores = servicioUsuario.Listar((int)TipoPerfil.Productor, "FRT");

            foreach (var item in productores)
            {
                Productor usuario;
                usuario = (Productor)servicioUsuario.Leer(item.IdUsuario, item.IdPerfil);
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
                readText = readText.Replace("{@TituloPagina}", "Proceso Nuevo");
                readText = readText.Replace("{@Titulo}", "Nuevo Proceso de Compra");
                readText = readText.Replace("{@SubTitulo}", "Aviso Proceso de Compra");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + item.Nombre + " " + item.Apellido + "</b>:<br />Se ha creado un nuevo proceso de compra internacional. Le informamos a usted <b>"+ usuario.NombreProductor + "</b>");

                servicioCorreo.Asunto = "[Procesos] - Nuevo Proceso Creado";
                servicioCorreo.Enviar(readText, item.Email);
            }
            return new RespuestaProceso() { Exito = true, Mensaje = "Creación Exitosa", Proceso = proceso };
        }
        public RespuestaProcesoListar Listar()
        {
            var respuesta = servicioProceso.Listar();
            if (respuesta == null)
                return new RespuestaProcesoListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de traer los Procesos" };
            if (respuesta.Count.Equals(0))
                return new RespuestaProcesoListar() { Exito = false, Mensaje = "No hay procesos para listar" };

            return new RespuestaProcesoListar() { Procesos = respuesta };

        }
        public RespuestaOferta CrearOferta(Oferta oferta)
        {
            var resultado = servicioOferta.Leer(oferta.IdProducto, oferta.IdProceso);
            if (resultado != null)
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "Ya se ha realizado una oferta por este producto en este proceso" };
            }
            var respuesta = servicioOferta.Crear(oferta);
            if (respuesta == 0)
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar la oferta" };
            }
            return new RespuestaOferta() { Exito = true, Mensaje = "Creación Exitosa", Oferta = servicioOferta.Leer(oferta.IdProducto, oferta.IdProceso) };
        }
        public RespuestaOferta LeerOferta(int idOferta)
        {
            var resultado = servicioOferta.Leer(idOferta);
            if (resultado == null)
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "Ha ocurrido un error validando la oferta" };
            }
            if (resultado.IdOferta.Equals(0))
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "No existe Oferta solicitada" };
            }
            return new RespuestaOferta() { Exito = true, Mensaje = "Creación Exitosa", Oferta = resultado };
        }

        public RespuestaOfertaListar ListarOferta()
        {
            var respuesta = servicioOferta.Listar();
            if (respuesta == null)
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de traer las ofertas" };
            if (respuesta.Count.Equals(0))
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "No hay ofertas para listar" };

            return new RespuestaOfertaListar() { Ofertas = respuesta };

        }
        public RespuestaOfertaListar ListarOferta(int idProductor)
        {
            var respuesta = servicioOferta.Listar(idProductor);
            if (respuesta == null)
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de traer las ofertas" };
            if (respuesta.Count.Equals(0))
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "No hay ofertas para listar" };

            return new RespuestaOfertaListar() { Ofertas = respuesta };

        }
    }
}
