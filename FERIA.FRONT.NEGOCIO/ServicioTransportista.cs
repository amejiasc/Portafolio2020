using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioTransportista
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaCamion LeerCamion(int id, string idSession)
        {
            RespuestaCamion respuesta = new RespuestaCamion();
            var respuestaApi = servicio.Get("api/Transportista/Camion/" + id, new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaCamionListar ListarCamiones(int IdUsuario, string idSession)
        {
            RespuestaCamionListar respuesta = new RespuestaCamionListar();
            var respuestaApi = servicio.Get("api/Transportista/Camion/" + IdUsuario + "/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaOrdenListar ListarCamiones(string idSession)
        {
            RespuestaOrdenListar respuesta = new RespuestaOrdenListar();
            var respuestaApi = servicio.Get("api/Transportista/Camion/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaCamion CrearCamion(Camion camion, string idSession)
        {
            RespuestaCamion respuesta = new RespuestaCamion();
            var respuestaApi = servicio.Post("api/Transportista/Camion", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            camion);
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        public RespuestaCamion ModificarCamion(Camion camion, string idSession)
        {
            RespuestaCamion respuesta = new RespuestaCamion();
            var respuestaApi = servicio.Post("api/Transportista/Camion", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            camion);
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }

        #region "Subastas"
        public RespuestaSubastaListar ListarSubastasByIdUsuario(int IdUsuario, string idSession)
        {
            RespuestaSubastaListar respuesta = new RespuestaSubastaListar();
            var respuestaApi = servicio.Get("api/Transportista/Subasta/" + IdUsuario + "/Listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
            return msj;
        }
        #endregion

    }
}

