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
        [Route("api/usuario/perfil")]
        public RespuestaUsuarioListar GetListaxPerfil(int idperfil, string idSession, string servicio="FRT")
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "No posee acceso valido", Usuarios = new List<Usuario>() };
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario();
            return servicioUsuario.ListarUsuarios(idperfil, servicio);
        }

        // GET: api/Usuario/5
        [Route("api/usuario/{id}")]
        public RespuestaUsuario Get(int id, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "No posee acceso valido", Usuario = new Usuario() };
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario();
            return servicioUsuario.Leer(id);
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

        
        [HttpPost]
        [Route("api/usuario/{id}/modificar")]
        public JObject PostUpdate(int id, [FromBody]JObject usuario, string idSession=null)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return JObject.FromObject(new RespuestaUsuario() { Exito = false, Mensaje = "No posee acceso valido", Usuario = new Usuario() });
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario(idSession);
            Usuario usuarioObj;
            try
            {
                usuarioObj = usuario.ToObject<Usuario>(); 
            }
            catch (Exception)
            {

                return JObject.FromObject(new RespuestaUsuario() { Exito = false, Mensaje = "Objeto enviado no corresponde con lo solicitado", Usuario = new Usuario() });
            }


            return JObject.FromObject(servicioUsuario.Modificar(usuarioObj));

        }
        [HttpPost]
        [Route("api/usuario/{id}/cambiarclave")]
        public JObject PostChangePassword(int id, [FromBody]JObject usuario, string idSession)
        {
            if (string.IsNullOrEmpty(idSession))
            {
                return JObject.FromObject(new RespuestaUsuario() { Exito = false, Mensaje = "No posee acceso valido", Usuario = new Usuario() });
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario(idSession);
            Usuario usuarioObj;
            try
            {
                usuarioObj = usuario.ToObject<Usuario>();
            }
            catch (Exception)
            {

                return JObject.FromObject(new RespuestaUsuario() { Exito = false, Mensaje = "Objeto enviado no corresponde con lo solicitado", Usuario = new Usuario() });
            }


            return JObject.FromObject(servicioUsuario.ModificarClave(usuarioObj));

        }


        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
