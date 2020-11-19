using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Views
{
    [Authorize(Roles = "5")]
    public class CamionController : Controller
    {
        ServicioTransportista servicioTransportista;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public CamionController()
        {
            servicioTransportista = new ServicioTransportista();
        }
        // GET: Camion
        public ActionResult Listar(string mensaje = "", bool exito = false)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = exito;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioTransportista.ListarCamiones(Login.IdUsuario, Login.SesionId);
            if (respuesta.Exito)
                return View(respuesta.Camiones);
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View(new List<Camion>());
            }
        }
        public ActionResult Mantener(int id = 0)
        {
            if (id != 0)
            {
                var respuesta = servicioTransportista.LeerCamion(id, Login.SesionId);
                if (!respuesta.Exito)
                {
                    return RedirectToAction("Listar", new { respuesta.Mensaje });
                }
                return View(respuesta.Camion);
            }
            else
            {
                return View(new Camion());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Mantener(Camion camion, string RefrigeraEdit="false")
        {

            camion.Refrigera = RefrigeraEdit.Equals("true");
            if (camion.IdCamion == 0)
            {
                camion.IdUsuario = Login.IdUsuario;
                var respuesta = servicioTransportista.CrearCamion(camion, Login.SesionId);
                ViewBag.Exito = respuesta.Exito;
                ViewBag.Mensaje = respuesta.Mensaje;
                if (!ViewBag.Exito)
                    return View(camion);
                return View(respuesta.Camion);
            }
            else
            {
                var respuesta = servicioTransportista.ModificarCamion(camion, Login.SesionId);
                ViewBag.Exito = respuesta.Exito;
                ViewBag.Mensaje = respuesta.Mensaje;
                if (!ViewBag.Exito)
                    return View(camion);

                return View(respuesta.Camion);
            }
        }
    }
}