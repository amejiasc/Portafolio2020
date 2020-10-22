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
        public int IdUsuario { get; set; }

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
}
