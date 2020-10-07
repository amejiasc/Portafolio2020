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
        // GET: api/Login
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            //var Usuario = new NEGOCIO.ServicioUsuario().Leer(1);
            return NEGOCIO.Funciones.Encripta.EncodePassword("12345a");
        }

        // POST: api/Login
        [HttpPost]        
        public JObject Post([FromBody]CLASES.Login usuario)
        {   
            ServicioLogin servicioLogin = new ServicioLogin();
            usuario.Clave = NEGOCIO.Funciones.Encripta.EncodePassword(usuario.Clave);

            CLASES.Usuario respuesta = new CLASES.Usuario();


            return servicioLogin.Login(usuario.Rut, usuario.Clave, Request, usuario.TipoPerfil);
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
