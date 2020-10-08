using FERIA.CLASES;
using FERIA.NEGOCIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class LoginController : ApiController
    {
        
        // POST: api/Login
        [HttpPost]        
        public JObject Post([FromBody]CLASES.Login usuario)
        {   
            ServicioLogin servicioLogin = new ServicioLogin();
            usuario.Clave = NEGOCIO.Funciones.Encripta.EncodePassword(usuario.Clave);

            CLASES.Usuario respuesta = new CLASES.Usuario();


            return servicioLogin.Login(usuario.Rut, usuario.Clave, Request, usuario.TipoPerfil);
        }

        /// <summary>
        /// Post con variables de identificación de recurso
        /// </summary>
        /// <param name="RandomObj">Cualquier objeto serializado</param>
        /// <param name="tipo">Indica la acción a realizar</param>
        /// <param name="IdSession">Cuando el usuario esta logueado se debe informar</param>
        /// <returns></returns>
        [HttpPost]      

        public JObject Post([FromBody]JObject RandomObj, string tipo, string IdSession="")
        {   
            ServicioLogin servicioLogin = new ServicioLogin(IdSession);
            CLASES.Usuario respuesta = new CLASES.Usuario();
            
            if (tipo=="CambiarClave")
            {
                var miObjeto = RandomObj.ToObject<CLASES.CambiarClave>();
                return JObject.FromObject(servicioLogin.CambiarClave(miObjeto.IdUsuario, miObjeto.ClaveProvisoria,  miObjeto.ClaveNueva));
            }

            return null;
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
 

}
