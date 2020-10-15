using FERIA.API.App_Start;
using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class UtilController : ApiController
    {
        [HttpGet]
        [Route("api/util/categorias")]
        public List<FERIA.CLASES.Categoria> ListarCategorias()
        {
            return UtilConfig.ListarCategorias();
        }
        [HttpGet]
        [Route("api/util/perfil")]
        public List<FERIA.CLASES.Perfil> ListarPerfiles()
        {
            return UtilConfig.ListarPerfiles();
        }
        [HttpGet]
        [Route("api/util/Estado/{tipo}")]
        public List<string> ListarEstados(string tipo)
        {
            ServicioUtil servicioUtil = new ServicioUtil();
            if (tipo.ToUpper().Equals("ORDEN"))
            {
                return servicioUtil.EstadosOrdenes();
            }
            return new List<string>();
        }
    }
}
