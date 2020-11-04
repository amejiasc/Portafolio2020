using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    public class UtilsController : Controller
    {
        // GET: Utils
        Usuario Login = Helper.Autenticacion.TraerUsuarioAutenticado();
        public ActionResult Comuna(string _region)
        {
            if (string.IsNullOrEmpty(_region))
            {
                return PartialView(new List<Comuna>());
            }
            List<Comuna> comunas = UtilConfig.Comunas().Where(x => x.IdRegion.Equals(int.Parse(_region))).ToList();
            return PartialView(comunas);
        }
        public ActionResult Orden(int _idOrden)
        {
            ServicioOrden servicio = new ServicioOrden(); 
            if (_idOrden.Equals(0))
            {
                return PartialView(new Orden());
            }
            Orden orden = servicio.Leer(_idOrden, Login.SesionId).Orden;
            return PartialView(orden);
        }

    }
}