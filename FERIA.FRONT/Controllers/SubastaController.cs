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
        ServicioTransportista servicioTransportista;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public SubastaController()
        {
            servicioSubasta = new ServicioSubasta();
            servicioTransportista = new ServicioTransportista();
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
        public ActionResult Listar(string mensaje = "", bool exito = false)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = exito;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioTransportista.ListarSubastasByIdUsuario(Login.IdUsuario, Login.SesionId);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ofertar(DetalleSubasta detalleSubasta)
        {
            if (detalleSubasta.MontoOferta.Equals(0)) 
            {
                return RedirectToAction("Ofertar", new { id=detalleSubasta.IdSubasta, mensaje= "Monto Oferta no puede ser 0" });
            }
           
            var respuesta = servicioSubasta.Ofertar(detalleSubasta, Login.SesionId);
            if (respuesta.Exito)
            {
                return RedirectToAction("Participar", new { exito = true, mensaje = "Oferta realizada con éxito" });
            }
            else
            {
                var respuesta1 = servicioSubasta.Leer(detalleSubasta.IdSubasta, Login.SesionId);
                ViewBag.Mensaje = respuesta1.Mensaje;
                ViewBag.Exito = respuesta1.Exito;
                return View(respuesta1.Subasta);
            }
        }

    }
}