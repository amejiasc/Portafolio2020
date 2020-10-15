using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{    
    public class ServicioOrden
    {
        STORE.ServicioUsuario servicioUsuario;
        public ServicioOrden(string IdSession = "")
        {
            this.servicioUsuario = new STORE.ServicioUsuario(IdSession);            
        }

    }
}
