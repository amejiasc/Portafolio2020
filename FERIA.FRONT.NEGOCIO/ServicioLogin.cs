using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioLogin
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaLogin Login(Login login) 
        {
            RespuestaLogin respuestaLogin = new RespuestaLogin();
            var respuesta = servicio.Post("api/login", new List<RestSharp.Parameter>(), login);
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaLogin);
            return msj;
        }
        public RespuestaUsuario CambiarClave(int IdUsuario, string ClaveProvisoria, string Clave, string ReClave, string IdSession)
        {
            RespuestaUsuario respuestaUsuario = new RespuestaUsuario();
            var respuesta = servicio.Post("api/login", 
                                         new List<RestSharp.Parameter>() {
                                            new RestSharp.Parameter() { Name = "tipo", Value = "CambiarClave" },
                                            new RestSharp.Parameter() { Name = "IdSession", Value = IdSession }
                                         }, 
                                         new CLASES.CambiarClave() { 
                                             IdUsuario = IdUsuario,
                                             ClaveProvisoria = ClaveProvisoria,
                                             ClaveNueva = Clave,
                                             ReClaveNueva = ReClave 
                                         });
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuario);
            return msj;
        }
        public RespuestaUsuario Recuperar(Recuperar recuperar)
        {
            RespuestaUsuario respuestaLogin = new RespuestaUsuario();
            var respuesta = servicio.Post("api/Recuperar", new List<RestSharp.Parameter>(), recuperar);
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaLogin);
            return msj;
        }
    }
}
