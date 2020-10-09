using FERIA.CLASES;
using FERIA.NEGOCIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        public JObject Post([FromBody]JObject usuario, string capa, int perfil)
        {
            ServicioUsuario servicioUsuario = new ServicioUsuario();
            CLASES.Usuario usuarioObj;
            switch (perfil)
            {
                case (int)TipoPerfil.Administrador:
                    usuarioObj = usuario.ToObject<CLASES.Administrador>();
                    break;
                case (int)TipoPerfil.Productor:
                    usuarioObj = usuario.ToObject<CLASES.Productor>();
                    break;
                case (int)TipoPerfil.Cliente_Externo:
                    usuarioObj = usuario.ToObject<CLASES.ClienteExterno >();
                    break;
                case (int)TipoPerfil.Cliente_Interno:
                    usuarioObj = usuario.ToObject<CLASES.ClienteInterno>();
                    break;
                case (int)TipoPerfil.Transportista:
                    usuarioObj = usuario.ToObject<CLASES.Transportista>();
                    break;
                case (int)TipoPerfil.Consultor:
                    usuarioObj = usuario.ToObject<CLASES.Usuario>();
                    break;
                default:
                    return JObject.Parse("{Exito:false, Mensaje:\"Tipo no es válido\"}");
            }

            return JObject.FromObject(servicioUsuario.Crear(usuarioObj, capa));
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
