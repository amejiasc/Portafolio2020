using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class ClienteExterno : Usuario
    {
        public string NombreEmpresa { get; set; }
        public string Pais{ get; set; }
        public string Ciudad { get; set; }
    }
}
