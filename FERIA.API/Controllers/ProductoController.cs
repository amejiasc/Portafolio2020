using FERIA.CLASES;
using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]        
        public RespuestaProductoListar Get(int id)
        {
            return new ServicioProducto().Listar(id);
        }

    }
}
