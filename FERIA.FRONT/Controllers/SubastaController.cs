using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize(Roles = "5")]
    public class SubastaController : Controller
    {   
        ServicioSubasta servicioSubasta;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public SubastaController()
        {
            servicioSubasta = new ServicioSubasta();
        }

        // GET: Subasta
        public ActionResult Participar(string mensaje = "", bool exito = false)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = exito;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioSubasta.ListarActivas(Login.SesionId);
            if (respuesta.Exito)
                return View(respuesta.Subastas);
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View(new List<Subasta>());
            }
        }

        [HttpGet]
        public ActionResult Ofertar(int id, string mensaje = "")
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = false;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioSubasta.Leer(id, Login.SesionId);
            if (respuesta.Exito)
            {
                return View(respuesta.Subasta);
            }
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View("Participar", new List<Subasta>());
            }
        }

    }
}