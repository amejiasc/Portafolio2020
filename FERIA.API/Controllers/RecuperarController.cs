using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class RecuperarController : ApiController
    {
        // GET: api/Recuperar
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Recuperar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Recuperar
        [HttpPost]
        public CLASES.RespuestaUsuario Post([FromBody]CLASES.Recuperar recuperar)
        {
            ServicioLogin servicioLogin = new ServicioLogin();
            return servicioLogin.Recuperar(recuperar.Rut, recuperar.Email, recuperar.TipoPerfil, recuperar.Servicio);
        }

        // PUT: api/Recuperar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Recuperar/5
        public void Delete(int id)
        {
        }
    }
}
