using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioUtil
    {
        STORE.ServicioUtil servicioUtil;
        public ServicioUtil(string idSession = "")
        {
            this.servicioUtil = new STORE.ServicioUtil(idSession);
        }
        public List<Region> Regiones()
        {
            return servicioUtil.ListarRegiones();
        }
        public List<Comuna> Comunas()
        {
            return servicioUtil.ListarComunas();
        }
        public List<Categoria> Categorias()
        {
            return servicioUtil.ListarCategorias();
        }
        public List<Perfil> Perfiles()
        {
            return servicioUtil.ListarPerfiles ();
        }
        public List<string> EstadosOrdenes() 
        {
            return "ANULADO,CERRADO,PENDIENTE,VIGENTE".ToString().Split(',').ToList();        
        }

    }
}
