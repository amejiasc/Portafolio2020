using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    public class HomeController : Controller
    {
        ServicioLogin servicioLogin;
        public HomeController()
        {
            servicioLogin = new ServicioLogin();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(string rut, string clave, int perfil)
        {
            var Respuesta = servicioLogin.Login(new CLASES.Login() { Clave=clave, Rut=rut, TipoPerfil = perfil  });
            if (Respuesta.Exito && Respuesta.Motivo == MotivoNoExitoLogin.Exito)
            {
                Helper.Autenticacion.Login(Respuesta.Usuario);
                return RedirectToAction("Index", "Sistema");

            }
            else if (Respuesta.Exito && Respuesta.Motivo == MotivoNoExitoLogin.UsuarioDebeCambiarClave)
            {
                return RedirectToAction("CambiarClave", "Home", Respuesta.Usuario);
            }
            ViewBag.Error = true;
            ViewBag.Mensaje = Respuesta.Mensaje;
            return View("Index");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(bool cerrarSesion = false)
        {
            ViewBag.CerrarSesion = cerrarSesion;
            return View("Index");
        }
        [HttpPost]
        public ActionResult Recuperar(string Rut, string Email)
        {
            var Respuesta = servicioLogin.Recuperar(new CLASES.Recuperar() { Email=Email, Rut= Rut, Servicio="FRT", TipoPerfil=0  });
            if (Respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha enviado la clave nueva a su Email.";
                return View("Index");

            }
            ViewBag.Error = true;
            ViewBag.Mensaje = Respuesta.Mensaje;
            return View("Index");
        }
        public ActionResult CambiarClave(Usuario usuario)
        {
            Session["usuario"] = usuario;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult CambiarClave(int IdUsuario, string ClaveProvisoria, string Clave, string ReClave)
        {
            var usuario = (Usuario)Session["usuario"];
            var respuesta = servicioLogin.CambiarClave(IdUsuario, ClaveProvisoria, Clave, ReClave, usuario.SesionId);
            if (respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha modificado la clave correctamente";
                return View("Index");
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = respuesta.Mensaje;
                return View("CambiarClave", Session["usuario"]);
            }

        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Registro()
        {
            return View();
        }


    }
}