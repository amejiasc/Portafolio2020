using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize(Roles = "2")]
    public class ProcesoController : Controller
    {

        ServicioProceso servicioProceso;
        ServicioOferta servicioOferta;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public ProcesoController()
        {
            servicioProceso = new ServicioProceso();
            servicioOferta = new ServicioOferta();
        }


        // GET: Proceso
        public ActionResult MisProcesos(string mensaje = "", bool exito = false)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = exito;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioProceso.ListarByIdProductor(Login.IdUsuario, Login.SesionId);
            if (respuesta.Exito)
                return View(respuesta.Procesos);
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View(new List<Proceso>());
            }
        }
        public ActionResult Postular(string mensaje = "", bool exito = false)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = exito;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioProceso.Listar(Login.SesionId, "PENDIENTE");
            if (respuesta.Exito)
                return View(respuesta.Procesos);
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View(new List<Proceso>());
            }
        }
        [HttpGet]
        public ActionResult ProcesoPostular(int id, string mensaje = "")
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = false;
                ViewBag.Mensaje = mensaje;
            }
            var respuesta = servicioProceso.Leer(id, Login.SesionId);
            if (respuesta.Exito)
            {
                respuesta.Proceso.IdUsuario = Login.IdUsuario;
                return View(respuesta.Proceso);
            }
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View("Postular", new List<Proceso>());
            }
        }
        [HttpPost]
        public ActionResult ProcesoPostular(Models.Proceso proceso)
        {
            int error = 0;
            string msg = "";
            foreach (var item in proceso.DetalleOferta)
            {
                Oferta oferta = new Oferta()
                {
                    Cantidad = item.Cantidad,
                    IdProducto = item.IdProducto,
                    IdProceso = proceso.IdProceso,
                    ValorUnitario = item.ValorUnitario,
                    IdUsuario = Login.IdUsuario
                };
                var resultado = servicioOferta.Crear(oferta, Login.SesionId);
                if (!resultado.Exito) {
                    error += 1;
                    msg += resultado.Mensaje + ", ";
                }
            }
            if (proceso.DetalleOferta.Count == error)
            {
                return RedirectToAction("ProcesoPostular", new { id = proceso.IdProceso, mensaje = "No fue posible ofertar.- Posibles Errores:- " + msg });
            }
            else {
                return RedirectToAction("Postular", new { exito = true, mensaje = "Oferta realizada con éxito" });
            }
        }
    }
}