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
            if (!new ServicioUtil().EstadosProcesos().Exists(x => x.Contains(proceso.EstadoProceso))) 
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Estado enviado para el proceso no es válido" };
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
            if (!new ServicioUtil().EstadosProcesos().Exists(x => x.Contains(proceso.EstadoProceso)))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "Estado enviado para el proceso no es válido" };
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

            var procesoMail = servicioProceso.Leer(proceso.IdProceso);

            foreach (var item in productores)
            {
                Productor usuario;
                usuario = (Productor)servicioUsuario.Leer(item.IdUsuario, item.IdPerfil);
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Proceso);
                readText = readText.Replace("{@TituloPagina}", "Proceso Nuevo");
                readText = readText.Replace("{@Titulo}", "Nuevo Proceso de Compra");
                readText = readText.Replace("{@SubTitulo}", "Aviso Proceso de Compra");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + item.Nombre + " " + item.Apellido + "</b>:<br />Se ha creado un nuevo proceso de compra internacional. Le informamos a usted <b>"+ usuario.NombreProductor + "</b>");
                readText = readText.Replace("{@CodigoProceso}", procesoMail.IdProceso.ToString());
                readText = readText.Replace("{@FechaTerminoProceso}", procesoMail.FechaFinProceso.ToString("dd-MM-yyyy"));
                readText = readText.Replace("{@ComisionProceso}", procesoMail.Comision.ToString() + "%");
                readText = readText.Replace("{@ValorAduanaProceso}", procesoMail.ValorAduana.ToString("#0,00"));
                readText = readText.Replace("{@PagoServicioProceso}", procesoMail.PagoPorServicio.ToString("#0,00"));

                string detalleOrden = "";
                foreach (var detalle in procesoMail.Orden.DetalleOrden)
                {
                    detalleOrden += "<tr>";
                    detalleOrden += "<td>"+ detalle.NombreCategoria  +"</td>";
                    detalleOrden += "<td align='center'>" + detalle.Cantidad.ToString("#0,00") + "</td>";
                    detalleOrden += "</tr>";
                }
                readText = readText.Replace("{@TablaDetalleOrden}", detalleOrden);

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
        public bool BuscaGanadorProceso(int idProceso=0)
        {
            var respuesta = servicioProceso.Listar();
            List<Proceso> listar;
            if (idProceso.Equals(0))
            {
                listar = respuesta.Where(x => x.EstadoProceso.Equals("PENDIENTE") && DateTime.Parse(x.FechaFinProceso.ToString("yyyy-MM-dd")) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).ToList();
                //listar = respuesta.Where(x => x.EstadoProceso.Equals("PENDIENTE") ).ToList();
            }
            else {
                listar = respuesta.Where(x => x.EstadoProceso.Equals("PENDIENTE") && x.IdProceso.Equals(idProceso)).ToList();
            }

            var productos = new STORE.ServicioProducto().Listar();
            foreach (var item in listar)
            {
                bool cumple = false;
                //var ofertas = item.Ofertas;
                var detalleOrden = item.Orden.DetalleOrden;
                foreach (var detalle in detalleOrden)
                {
                    var oferta = item.Ofertas.Where(x => x.IdCategoria.Equals(detalle.IdCategoria)).Sum(x => x.Cantidad);
                    if (oferta >= detalle.Cantidad)
                    {
                        cumple = true;
                    }
                    else {
                        cumple = false;
                        break;
                    }
                }
                if (cumple) 
                {
                    foreach (var detalle in detalleOrden)
                    {
                        var ofertas = item.Ofertas.Where(x => x.IdCategoria.Equals(detalle.IdCategoria)).OrderByDescending(x => x.Calidad).OrderBy(x => x.ValorUnitario).ToList();
                        var cantidad = 0;
                        foreach (var oferta in ofertas)
                        {
                            if (cantidad <= detalle.Cantidad)
                            {
                                servicioOferta.ActualizaEstadoOferta(oferta.IdOferta, "GANADA");
                                cantidad += oferta.Cantidad;
                                EnviarCorreoGanador(item.IdProceso, oferta.IdUsuario);
                            }
                            else 
                            {
                                servicioOferta.ActualizaEstadoOferta(oferta.IdOferta, "CANCELADA", oferta.Cantidad, oferta.IdProducto);
                            }
                            
                        }
                    }
                    var cambioEstadoProceso = servicioProceso.ActualizaEstadoProceso(item.IdProceso, "EN_PROCESO");                    
                }
            }
            return true;
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

        private void EnviarCorreoGanador(int idProceso, int idProductor)
        {
            var productores = servicioUsuario.Listar((int)TipoPerfil.Productor, "FRT");

            var procesoMail = servicioProceso.Leer(idProceso);

            foreach (var item in productores.Where(x=>x.IdUsuario.Equals(idProductor)).ToList())
            {
                Productor usuario;
                usuario = (Productor)servicioUsuario.Leer(item.IdUsuario, item.IdPerfil);
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Proceso);
                readText = readText.Replace("{@TituloPagina}", "Proceso Ganador");
                readText = readText.Replace("{@Titulo}", "Ha Ganado el Proceso");
                readText = readText.Replace("{@SubTitulo}", "Aviso de Triunfo");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + item.Nombre + " " + item.Apellido + "</b>:<br />Se le informa que ha sido ganador del proceso identificado más abajo. Le informamos a usted <b>" + usuario.NombreProductor + "</b>");
                readText = readText.Replace("{@CodigoProceso}", procesoMail.IdProceso.ToString());
                readText = readText.Replace("{@FechaTerminoProceso}", procesoMail.FechaFinProceso.ToString("dd-MM-yyyy"));
                readText = readText.Replace("{@ComisionProceso}", procesoMail.Comision.ToString() + "%");
                readText = readText.Replace("{@ValorAduanaProceso}", procesoMail.ValorAduana.ToString("#0,00"));
                readText = readText.Replace("{@PagoServicioProceso}", procesoMail.PagoPorServicio.ToString("#0,00"));

                string detalleOrden = "";
                foreach (var detalle in procesoMail.Orden.DetalleOrden)
                {
                    detalleOrden += "<tr>";
                    detalleOrden += "<td>" + detalle.NombreCategoria + "</td>";
                    detalleOrden += "<td align='center'>" + detalle.Cantidad.ToString("#0,00") + "</td>";
                    detalleOrden += "</tr>";
                }
                readText = readText.Replace("{@TablaDetalleOrden}", detalleOrden);

                servicioCorreo.Asunto = "[Procesos] - Proceso Ganado";
                servicioCorreo.Enviar(readText, item.Email);
            }            
        }
    }
}
