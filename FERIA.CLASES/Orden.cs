using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public DateTime FechaOrden { get; set; }
        public bool FirmaContrato { get; set; }
        public DateTime FechaFirmaContrato { get; set; }
        public string Estado { get; set; }
        public double PrecioVenta { get; set; }
        public int IdClienteExterno { get; set; }
        public int IdClienteInterno { get; set; }
        public List<DetalleOrden> detalleOrden { get; set; }

    }
    public class DetalleOrden {
        public int Cantidad { get; set; }
        public int IdOrden { get; set; }
        public double Monto { get; set; }
        public int IdCategoria { get; set; }

    }

}
