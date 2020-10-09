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
    }
}