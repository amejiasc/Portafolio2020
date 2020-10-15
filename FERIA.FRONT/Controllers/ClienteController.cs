using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize(Roles = "3,4")]
    public class ClienteController : Controller
    {
        ServicioOrden servicioOrden;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public ClienteController() 
        {
            servicioOrden = new ServicioOrden();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantener(int id = 0)
        {
            if (id != 0)
            {
                var respuesta = servicioOrden.Leer(id, Login.SesionId  );
                if (!respuesta.Exito)
                {
                    return RedirectToAction("Index", new { respuesta.Mensaje });
                }
                return View(respuesta.Orden);
            }
            else
            {
                return View(new Orden());
            }
        }

    }
}