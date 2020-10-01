using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FERIA.FRONT.Helper
{
    public class Autenticacion
    {
        public static void DestruirSesionUsuario()
        {
            FormsAuthentication.SignOut();
        }

        public static Usuario TraerUsuarioAutenticado()
        {
            Usuario UsuarioAutenticado = new Usuario();
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    UsuarioAutenticado = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(ticket.UserData);
                    HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, UsuarioAutenticado.IdPerfil.ToString().Split('|'));
                }
            }
            return UsuarioAutenticado;
        }

        public static void Login(Usuario usuarioAutenticado)
        {
            DestruirSesionUsuario();

            HttpCookie Cookie = FormsAuthentication.GetAuthCookie("FERIA.VIRTUAL.FRONT", false);

            Cookie.Name = FormsAuthentication.FormsCookieName;
            Cookie.Expires = DateTime.Now.AddMinutes(30);


            FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(Cookie.Value);
            FormsAuthenticationTicket NewTicket = new FormsAuthenticationTicket(Ticket.Version,
                                                                                Ticket.Name,
                                                                                Ticket.IssueDate,
                                                                                Ticket.Expiration,
                                                                                Ticket.IsPersistent,
                                                                                Newtonsoft.Json.JsonConvert.SerializeObject(usuarioAutenticado));

            Cookie.Value = FormsAuthentication.Encrypt(NewTicket);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }
    }
}