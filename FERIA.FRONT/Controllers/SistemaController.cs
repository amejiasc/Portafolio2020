using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize]
    public class SistemaController : Controller
    {
        ServicioUsuario servicioUsuario;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public SistemaController() {
            servicioUsuario = new ServicioUsuario();
        }

        // GET: Sistema
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CerrarSesion()
        {
            Helper.Autenticacion.DestruirSesionUsuario();
            return RedirectToAction("Login", "Home", new { cerrarSesion = true });
        }
        #region "PERFILES"
        public ActionResult Perfil(bool exito = false, bool error = false, string mensaje = "")
        {
            ViewBag.Exito = exito;
            ViewBag.Error = error;
            ViewBag.Mensaje = mensaje;
            var respuesta = servicioUsuario.Leer(Login.IdUsuario, Login.SesionId );
            if (exito)
            {
                respuesta.Usuario.SesionId = Login.SesionId;
                Helper.Autenticacion.Login(respuesta.Usuario);
            }
            return View(respuesta.Usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Clave))
            {
                usuario.Clave = "";
            }

            var respuesta = servicioUsuario.Modificar(usuario, Login.SesionId);
            if (respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha modificado correctamente";
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = respuesta.Mensaje;
            }
            return RedirectToAction("Perfil", new { exito = ViewBag.Exito, error = ViewBag.Error, mensaje = ViewBag.Mensaje });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerfilClave(Usuario usuario)
        {

            usuario.Clave = NEGOCIO.Funciones.Encripta.EncodePassword(usuario.Clave);
            usuario.ReClave = NEGOCIO.Funciones.Encripta.EncodePassword(usuario.ReClave);

            var respuesta = servicioUsuario.ModificarClave(usuario, Login.SesionId);
            if (respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha modificado la clave correctamente";
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = respuesta.Mensaje;
            }
            return RedirectToAction("Perfil", new { exito = ViewBag.Exito, error = ViewBag.Error, mensaje = ViewBag.Mensaje });
        }
        #endregion
    }
}