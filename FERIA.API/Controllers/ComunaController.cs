using FERIA.API.App_Start;
using FERIA.NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class ComunaController : ApiController
    {
        
        
        // GET: api/Comuna
        public List<FERIA.CLASES.Comuna> Get()
        {          
            return UtilConfig.ListarComunas();
        }

        // GET: api/Comuna/5
        /// <summary>
        /// Obtiene la comuna 
        /// </summary>
        /// <param name="id">Id Comuna</param>
        /// <returns></returns>
        public FERIA.CLASES.Comuna Get(int id)
        {
            CLASES.Comuna comuna = new CLASES.Comuna();
            comuna = UtilConfig.ListarComunas().FirstOrDefault(x => x.IdComuna.Equals(id));

            return comuna;
        }
    }
}
