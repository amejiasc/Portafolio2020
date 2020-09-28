using FERIA.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class RegionController : ApiController
    {
        public List<FERIA.CLASES.Region> Get()
        {
            return UtilConfig.ListarRegiones();
        }

        // GET: api/Region/5
        /// <summary>
        /// Obtiene las comunas de la región indicada 
        /// </summary>
        /// <param name="id">Id Region</param>
        /// <returns></returns>
        public FERIA.CLASES.Region Get(int id)
        {
            CLASES.Region Region = new CLASES.Region();
            Region = UtilConfig.ListarRegiones().FirstOrDefault(x => x.IdRegion.Equals(id));

            return Region;
        }

        // POST: api/Region
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Region/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Region/5
        public void Delete(int id)
        {
        }
    }
}
