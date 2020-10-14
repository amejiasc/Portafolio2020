using FERIA.CLASES;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioUsuario
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaUsuario Crear(JObject usuario, string capa, int IdPerfil)
        {
            CLASES.Usuario usuarioObj;
            switch (IdPerfil)
            {
                case (int)TipoPerfil.Administrador:
                    usuarioObj = usuario.ToObject<CLASES.Administrador>();
                    break;
                case (int)TipoPerfil.Productor:
                    usuarioObj = usuario.ToObject<CLASES.Productor>();
                    break;
                case (int)TipoPerfil.Cliente_Externo:
                    usuarioObj = usuario.ToObject<CLASES.ClienteExterno>();
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
                    usuarioObj = new Usuario();
                    break;
            }
            RespuestaUsuario respuestaUsuario = new RespuestaUsuario();
            var respuesta = servicio.Post("api/usuario", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "capa", Value = capa },
                new RestSharp.Parameter() { Name = "perfil", Value = IdPerfil }
            },
            usuarioObj);
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuario);
            return msj;
        }
        public RespuestaUsuario Modificar(Usuario usuario, string idSession)
        {
            
            RespuestaUsuario respuestaUsuario = new RespuestaUsuario();
            var respuesta = servicio.Post("api/usuario/"+ usuario.IdUsuario +"/modificar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            usuario);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuario);
                return msj;
            }
            else
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema invocando el metodo" };
        }
        public RespuestaUsuario ModificarClave(CLASES.Usuario usuario, string idSession)
        {
            RespuestaUsuario respuestaUsuario = new RespuestaUsuario();
            var respuesta = servicio.Post("api/usuario/" + usuario.IdUsuario + "/cambiarclave", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            usuario);
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuario);
            return msj;
        }
        public RespuestaUsuarioListar ListarUsuarios(int idPerfil, string idSession)
        {
            RespuestaUsuarioListar respuestaUsuarioListar = new RespuestaUsuarioListar();
            var respuesta = servicio.Get("api/usuario/perfil", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idPerfil", Value = idPerfil },
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuarioListar);
            return msj;

        }
        
        public RespuestaUsuario Leer(int idUsuario, string idSession)
        {
            RespuestaUsuario respuestaUsuario = new RespuestaUsuario();
            var respuesta = servicio.Get("api/usuario/" + idUsuario, new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.Content, respuestaUsuario);
            return msj;
        }

    }
}
