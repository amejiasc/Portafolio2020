using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Subasta
    {
        public int IdSubasta { get; set; }
        public DateTime FechaSubasta { get; set; }
        public DateTime FechaTermino { get; set; }
        public bool Estado { get; set; }
        public int IdProceso { get; set; }
        public List<DetalleSubasta> DetalleSubasta { get; set; }
        public Subasta() 
        {
            Estado = true;
        }
    }
    public class DetalleSubasta
    {
        public int IdDetalle { get; set; }
        public int MontoOferta { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int IdSubasta { get; set; }
        public int IdUsuario { get; set; }
        public DetalleSubasta() {
            Estado = "EN_PROCESO";
        }
    }

    public class RespuestaSubastaListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Subasta> Subastas { get; set; }
        public RespuestaSubastaListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Subastas = new List<Subasta>();
        }
    }
    public class RespuestaSubasta
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Subasta Subasta { get; set; }
        public RespuestaSubasta()
        {
            Exito = true;
            Mensaje = string.Empty;
            Subasta = new Subasta();
        }
    }

}
