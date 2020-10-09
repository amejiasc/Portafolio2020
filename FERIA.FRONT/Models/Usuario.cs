using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FERIA.FRONT.Models
{
    public class Usuario : CLASES.Usuario
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }

        public string Pais { get; set; }
        public string Ciudad { get; set; }

    }
}