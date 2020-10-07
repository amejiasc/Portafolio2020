using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class ClienteInterno : Usuario
    {
        public string NombreCliente { get; set; }
        public string RutCliente { get; set; }
        public int IdComuna { get; set; }
    }
}
