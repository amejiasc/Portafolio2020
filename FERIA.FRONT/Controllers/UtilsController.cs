using FERIA.CLASES;
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
        public ActionResult Comuna(string _region)
        {
            if (string.IsNullOrEmpty(_region))
            {
                return PartialView(new List<Comuna>());
            }
            List<Comuna> comunas = UtilConfig.Comunas().Where(x => x.IdRegion.Equals(int.Parse(_region))).ToList();
            return PartialView(comunas);
        }

    }
}