using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize(Roles = "2")]
    public class ProductorController : Controller
    {

        ServicioProducto servicioProducto;
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public ProductorController()
        {
            servicioProducto = new ServicioProducto();
        }
        // GET: Productor
        public ActionResult Listar(string mensaje = "")
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Exito = false;
                ViewBag.Mensaje = mensaje;
            }

            var respuesta = servicioProducto.Listar(Login.IdUsuario, Login.SesionId);
            if (respuesta.Exito)
                return View(respuesta.Productos);
            else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                ViewBag.Exito = respuesta.Exito;
                return View(new List<Producto>());
            }
        }

        public ActionResult Mantener(int id = 0)
        {
            if (id != 0)
            {
                var respuesta = servicioProducto.Leer(id, Login.SesionId);
                if (!respuesta.Exito)
                {
                    return RedirectToAction("Listar", new { respuesta.Mensaje });
                }
                return View(respuesta.Producto);
            }
            else
            {
                return View(new Producto());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Mantener(Producto producto)
        {

            
            if (producto.IdProducto == 0)
            {
                producto.IdUsuario = Login.IdUsuario;
                var respuesta = servicioProducto.Crear(producto, Login.SesionId);
                ViewBag.Exito = respuesta.Exito;
                ViewBag.Mensaje = respuesta.Mensaje;
                if (!ViewBag.Exito)
                    return View(producto);
                return View(respuesta.Producto);
            }
            else
            {
                var respuesta = servicioProducto.Modificar(producto, Login.SesionId);
                ViewBag.Exito = respuesta.Exito;
                ViewBag.Mensaje = respuesta.Mensaje;
                if (!ViewBag.Exito)
                    return View(producto);

                return View(respuesta.Producto);
            }
        }


    }
}