using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    [Authorize(Roles = "2")]
    public class SubastaController : Controller
    {
        // GET: Subasta
        public ActionResult Index()
        {
            return View();
        }
    }
}