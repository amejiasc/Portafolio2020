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
    }
}
