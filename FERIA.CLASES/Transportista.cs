using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Transportista : Usuario 
    {
        public bool FirmaContrato { get; set; }
        public DateTime FechaTermino { get; set; }
        public string NombreTransportista { get; set; }
        public string RutTransportista { get; set; }
        
        public int IdComuna { get; set; }
    }
}
