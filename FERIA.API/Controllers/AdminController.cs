using FERIA.API.App_Start;
using FERIA.CLASES;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class AdminController : ApiController
    {
        NEGOCIO.ServicioOrden servicioOrden;
        NEGOCIO.ServicioProceso servicioProceso;
        NEGOCIO.ServicioSubasta servicioSubasta;
        [HttpPost]
        [Route("api/Admin/{IdOrden}/Firmar")]        
        public RespuestaFirmaOrden PostFirma(int IdOrden, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "No posee acceso valido"};
            }
            servicioOrden = new NEGOCIO.ServicioOrden(idSession);
            return servicioOrden.Firmar(IdOrden);
        }
        [HttpPost]
        [Route("api/Admin/Proceso")]
        public RespuestaProceso PostProceso([FromBody]Proceso proceso, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            if (proceso.IdProceso.Equals(0))
            {
                return servicioProceso.Crear(proceso);
            }
            else {
                return servicioProceso.Modificar(proceso);
            }
        }
        [HttpGet]
        [Route("api/Admin/Proceso/Listar")]
        public RespuestaProcesoListar Get(string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProcesoListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.Listar();
        }
        
        [HttpGet]
        [Route("api/Admin/Proceso/Ganador")]
        public bool GetGanadores(string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return false;
                //return new RespuestaProcesoListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.BuscaGanadorProceso(0);
        }
        [HttpGet]
        [Route("api/Admin/Proceso/{IdProceso}/Ganador")]
        public bool GetGanadorbyIdProceso(int IdProceso, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return false;
                //return new RespuestaProcesoListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.BuscaGanadorProceso(IdProceso);
        }

        [HttpPost]
        [Route("api/Admin/Oferta")]
        public RespuestaOferta PostOferta([FromBody]Oferta oferta, string idSession=null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);            
            return servicioProceso.CrearOferta(oferta);
            
        }
        [HttpGet]
        [Route("api/Admin/Oferta/Listar")]
        public RespuestaOfertaListar GetListarOferta(string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.ListarOferta();
        }
        [HttpGet]
        [Route("api/Admin/Oferta/{IdProductor}/Listar")]
        public RespuestaOfertaListar GetListarOfertaByIdProductor(int IdProductor, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOfertaListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.ListarOferta(IdProductor);
        }
        [HttpGet]
        [Route("api/Admin/Oferta/{id}")]
        public RespuestaOferta GetOferta(int id, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaOferta() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.LeerOferta(id);
        }

        #region"ADMIN Categorias"
        [HttpPost]
        [Route("api/Admin/Categoria")]
        public RespuestaCategoria PostCategoria([FromBody] Categoria categoria, string idSession= null)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            UtilConfig.Destruir("Categorias");
            var servicioUtil = new NEGOCIO.ServicioUtil(idSession);            
            if (categoria.IdCategoria.Equals(0))
            {
                return servicioUtil.CrearCategoria(categoria);
            }
            else
            {
                return servicioUtil.ModificarCategoria(categoria);
            }
            
        }
        [HttpGet]
        [Route("api/Admin/Categoria/Listar")]
        public RespuestaCategoriaListar GetListarCategorias(string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCategoriaListar () { Exito = false, Mensaje = "No posee acceso valido" };
            }
            var servicioUtil = new NEGOCIO.ServicioUtil(idSession);
            return new RespuestaCategoriaListar() { Exito = true, Mensaje = "", Categorias = servicioUtil.Categorias() };
        }
        #endregion
        #region"ADMIN Subastas"
        [HttpPost]
        [Route("api/Admin/Subasta/Ofertar")]
        public RespuestaDetalleSubasta PostOfertaSubasta([FromBody] DetalleSubasta detalle, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaDetalleSubasta() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioSubasta = new NEGOCIO.ServicioSubasta(idSession);
            return servicioSubasta.CrearOferta(detalle);
        }
        [HttpPost]
        [Route("api/Admin/Subasta")]
        public RespuestaSubasta PostSubasta([FromBody] Subasta subasta, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioSubasta = new NEGOCIO.ServicioSubasta(idSession);
            if (subasta.IdSubasta.Equals(0))
            {
                return servicioSubasta.Crear(subasta);
            }
            else
            {
                return servicioSubasta.Modificar(subasta);
            }
        }
        [HttpGet]
        [Route("api/Admin/Subasta/Listar")]
        public RespuestaSubastaListar GetListarSubastas(string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioSubasta = new NEGOCIO.ServicioSubasta(idSession);
            return servicioSubasta.Listar();
        }
        [HttpGet]
        [Route("api/Admin/Subasta/{idProceso}/Listar")]
        public RespuestaSubastaListar GetListarSubastasByIdProceso(int idProceso, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioSubasta = new NEGOCIO.ServicioSubasta(idSession);
            return servicioSubasta.Listar(idProceso);
        }
        #endregion

    }
}
