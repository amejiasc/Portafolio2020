using FERIA.CLASES;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FERIA.NEGOCIO
{
    public class ServicioLogin
    {
        STORE.ServicioUsuario servicioUsuario;
        ServicioCorreo servicioCorreo;
        public ServicioLogin()
        {
            this.servicioUsuario = new STORE.ServicioUsuario();
            this.servicioCorreo = new ServicioCorreo();
        }
        public ServicioLogin(string IdSession)
        {
            this.servicioUsuario = new STORE.ServicioUsuario(IdSession);
            this.servicioCorreo = new ServicioCorreo();
        }

        public RespuestaUsuario CambiarClave(int idUsuario, string claveProvisoria, string ClaveNueva)
        {
            claveProvisoria = Funciones.Encripta.EncodePassword(claveProvisoria);
            ClaveNueva = Funciones.Encripta.EncodePassword(ClaveNueva);

            var usuario = servicioUsuario.Login(idUsuario, claveProvisoria);
            
            if (usuario == null)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de confirmar credencial" };
            }
            if (usuario.IdUsuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Clave provisoria no corresponde." };
            }

            var id = servicioUsuario.CambiarClave(idUsuario, ClaveNueva);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }

            return new RespuestaUsuario() { Exito = true };
        }

        /// <summary>
        /// Recuperar clave de usuario
        /// </summary>
        /// <param name="rut">Rut o Pasaporte el Usuario</param>
        /// <param name="email">Email deñ mismo usuario</param>
        /// <param name="tipoPerfil">Perfil del usuario</param>
        /// <param name="servicio">servicio FRT o BAK</param>
        /// <returns></returns>
        public RespuestaUsuario Recuperar(string rut, string email, int tipoPerfil, string servicio="BAK")
        {
            email = email.ToLower();
            if (!Funciones.Varias.ValidarRut(rut))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado no es válido" };
            }
            else
            {
                rut = Funciones.Varias.FormatearRut(rut).Replace(".", "");
            }
            if (!Funciones.Varias.ValidarEmail(email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }

            var usuario = servicioUsuario.Listar(tipoPerfil, servicio).FirstOrDefault(x => x.Rut.Equals(rut) && x.Email.ToLower().Equals(email) && x.Estado && x.Activo);
            if (usuario == null)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Datos ingresados no existen registrados" };
            }
            string NewClave = Funciones.Varias.RandomPassword();
            if (servicioUsuario.RecuperarClave(usuario.IdUsuario, Funciones.Encripta.EncodePassword(NewClave)) == 1)
            {
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
                readText = readText.Replace("{@TituloPagina}", "Recuperar Clave");
                readText = readText.Replace("{@Titulo}", "Recuperación de Clave");
                readText = readText.Replace("{@SubTitulo}", "Envío de Clave Provisoría");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + usuario.Nombre + " " + usuario.Apellido + "</b>:<br />A continuación enviamos clave provisoría que debe ser utilizada en el portal. Posterior a eso, se solicitará una nueva clave para que pueda recordarla.<br />Su clave: <b>" + NewClave + "</b>");

                servicioCorreo.Asunto = "[MAIPO GRANDE] - Recuperación de Clave";
                servicioCorreo.Enviar(readText, usuario.Email);

            }
            else
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "No Fue posible recuperar su clave. Intente de nuevo, más tarde" };
            }


            return new RespuestaUsuario() { Exito = true };
        }

        /// <summary>
        /// Valida si usuario existe o no en el sistema
        /// </summary>
        /// <param name="rut">Rut o Pasaporte el Usuario</param>
        /// <param name="clave">clave del usuario en el sistema</param>
        /// <param name="request">Datos de donde se conecta el usuario</param>
        /// <param name="tipoPerfil">Perfil de el usuario</param>
        /// <returns></returns>
        public JObject Login(string rut, string clave, HttpRequestMessage request, int tipoPerfil = 1)
        { 

            Dictionary<string, string> d = new Dictionary<string, string>();
            try
            {
                foreach (string s in ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.ServerVariables)
                {
                    if (s == "REMOTE_ADDR")
                    {
                        d.Add(s, ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.ServerVariables[s]);
                    }
                }
            }
            catch (Exception ex) { 
            
            }
            
            DataContractJsonSerializer serverVariablesSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
            MemoryStream ms = new System.IO.MemoryStream();
            serverVariablesSerializer.WriteObject(ms, d);
            string json = Encoding.Default.GetString(ms.ToArray());

            if (!tipoPerfil.Equals(3))
            {
                if (!Funciones.Varias.ValidarRut(rut))
                {
                    return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioRutInvalido, Mensaje = "Rut ingresado no es válido", Usuario = null });
                }
                else
                {
                    rut = Funciones.Varias.FormatearRut(rut).Replace(".", "");
                }
            }
            
            var usuario = servicioUsuario.Login(new CLASES.Login() { Clave = clave, Rut = rut, TipoPerfil = tipoPerfil });

            if (usuario == null)
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.ErrorNoControlado, Mensaje = "Ha ocurrido un error no controlado", Usuario = usuario });
            }
            if (usuario.IdUsuario == 0) /// Ve si hace reintentos
            {
                return Reintentos(rut, tipoPerfil, usuario);                
            }
            else
            {
                return GeneraSesion(usuario, json);                
            }
        }

        private JObject GeneraSesion(Usuario usuario, string json) 
        {
            if (!usuario.Activo)
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioNoVigente, Mensaje = "Usuario esta bloqueado. Solicite al administrador desbloquear", Usuario = usuario });
            }
            var guid = Guid.NewGuid().ToString();
            if (servicioUsuario.GeneraSesion(usuario.IdUsuario, usuario.IdPerfil, guid, json) == 1)
            {
                usuario.SesionId = guid;
            }
            else
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.ErrorNoControlado, Mensaje = "No fue posible generar la sesión", Usuario = usuario });
            }
            if (!usuario.Estado)
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioNoVigente, Mensaje = "Usuario no esta vigente", Usuario = usuario });
            }

            if (usuario.CambiaClave)
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = true, Motivo = MotivoNoExitoLogin.UsuarioDebeCambiarClave, Mensaje = "Usuario debe cambiar la clave", Usuario = usuario });
            }

            return JObject.FromObject(new RespuestaLogin() { Exito = true, Motivo = MotivoNoExitoLogin.Exito, Mensaje = string.Empty, Usuario = usuario });

        }

        private JObject Reintentos(string rut, int tipoPerfil, Usuario usuario) {
            var resultado = servicioUsuario.Reintentos(rut, tipoPerfil);
            if (resultado.Son.Equals(1))
            {
                if (resultado.Activo.Equals(false))
                {
                    return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioNoVigente, Mensaje = "Usuario se ha bloqueado", Usuario = usuario });
                }
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioClaveIncorrecta, Mensaje = "Usuario/Clave incorrecto", Usuario = usuario });
            }
            else
            {
                return JObject.FromObject(new RespuestaLogin() { Exito = false, Motivo = MotivoNoExitoLogin.UsuarioNoExiste, Mensaje = "Usuario no existe", Usuario = usuario });
            }
        }
    }
}
