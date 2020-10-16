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
        public List<DetalleOrden> DetalleOrden { get; set; }
        public Orden() {
            Estado = "PENDIENTE";
            FirmaContrato = false;
        }

    }
    public class DetalleOrden {
        public int Cantidad { get; set; }
        public int IdOrden { get; set; }
        public double Monto { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }

    public class RespuestaOrdenListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Orden> Ordenes { get; set; }
        public RespuestaOrdenListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Ordenes = new List<Orden>();
        }
    }
    public class RespuestaOrden
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Orden Orden { get; set; }
        public RespuestaOrden()
        {
            Exito = true;
            Mensaje = string.Empty;
            Orden = new Orden();
        }
    }

}
