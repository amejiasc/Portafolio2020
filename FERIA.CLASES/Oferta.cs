using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Oferta
    {
        public int IdOferta { get; set; }
        public int Cantidad { get; set; }
        public int ValorUnitario { get; set; }
        public int IdProceso { get; set; }
        public string Estado { get; set; }
        public DateTime FechaOferta { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }

        public Oferta() {
            IdOferta = 0;
            Estado = "INGRESADA";
        }
    }
    public class RespuestaOferta
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Oferta Oferta { get; set; }
        public RespuestaOferta()
        {
            Exito = true;
            Mensaje = string.Empty;
        }
    }
    public class RespuestaOfertaListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Oferta> Ofertas { get; set; }
        public RespuestaOfertaListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Ofertas = new List<Oferta>();
        }
    }
}
