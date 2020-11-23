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
    public class TransportistaController : ApiController
    {
        #region "CAMIONES"
        [HttpPost]
        [Route("api/Transportista/Camion")]
        public RespuestaCamion PostCamion([FromBody] Camion camion, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            var servicio = new ServicioTransportista(idSession);
            if (camion.IdCamion.Equals(0))
            {
                return servicio.CrearCamion(camion);
            }
            else
            {
                return servicio.ModificarCamion(camion);
            }
        }

        [HttpGet]
        [Route("api/Transportista/Camion/{IdUsuario}/Listar")]
        public RespuestaCamionListar GetListar(int IdUsuario, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCamionListar() { Exito = false, Mensaje = "No posee acceso valido", Camiones  = new List<Camion>() };
            }
            ServicioTransportista servicio = new ServicioTransportista(idSession);
            return servicio.ListarCamiones(IdUsuario);

        }
        [HttpGet]
        [Route("api/Transportista/Camion/Listar")]
        public RespuestaCamionListar GetListar(string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCamionListar() { Exito = false, Mensaje = "No posee acceso valido", Camiones = new List<Camion>() };
            }
            ServicioTransportista servicio = new ServicioTransportista(idSession);
            return servicio.ListarCamiones(0);
        }
        [HttpGet]
        [Route("api/Transportista/Camion/{id}")]
        public RespuestaCamion Get(int id, string idSession = "")
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "No posee acceso valido", Camion = new Camion() };
            }
            ServicioTransportista servicio = new ServicioTransportista(idSession);
            return servicio.LeerCamion(id);

        }
        #endregion
        [HttpGet]
        [Route("api/Transportista/Subasta/{IdUsuario}/Listar")]
        public RespuestaSubastaListar GetListarSubastasByIdUsuario(int IdUsuario, string idSession = null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            var servicioSubasta = new NEGOCIO.ServicioSubasta(idSession);
            return servicioSubasta.ListarByIdUsuario(IdUsuario);
        }


    }
}
