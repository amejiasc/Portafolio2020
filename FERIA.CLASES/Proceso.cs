using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Proceso
    {
        public int IdProceso { get; set; }
        public int Comision { get; set; }
        public int ValorAduana { get; set; }
        public int PagoPorServicio { get; set; }
        public int PagoTransportista { get; set; }
        public DateTime FechaProceso { get; set; }
        public DateTime FechaFinProceso { get; set; }
        public string EstadoProceso { get; set; }
        public int IdOrden { get; set; }

        public Orden Orden { get; set; }

        public List<Oferta> Ofertas { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaMaxSubasta { get; set; }

        public Proceso() {
            EstadoProceso = "PENDIENTE";
            Orden = new Orden();
            FechaMaxSubasta = DateTime.MinValue;
        }

    }
    public class RespuestaProceso
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Proceso Proceso { get; set; }
        public RespuestaProceso()
        {
            Exito = true;
            Mensaje = string.Empty;
        }
    }
    public class RespuestaProcesoListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Proceso> Procesos { get; set; }
        public RespuestaProcesoListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Procesos = new List<Proceso>();
        }
    }
}
