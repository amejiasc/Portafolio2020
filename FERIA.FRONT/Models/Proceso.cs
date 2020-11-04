using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FERIA.FRONT.Models
{
    public class Proceso
    {
        public int IdProceso { get; set; }
        public int IdUsuario { get; set; }
        public List<DetalleOferta> DetalleOferta { get; set; }
    }
    public class DetalleOferta
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int ValorUnitario { get; set; }
    }
}