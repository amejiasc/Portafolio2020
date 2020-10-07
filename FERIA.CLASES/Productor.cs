using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Productor : Usuario 
    {
        public bool FirmaContrato { get; set; }
        public DateTime FechaTermino { get; set; }
        public string NombreProductor { get; set; }
        public string RutProductor { get; set; }
        public bool PermiteSaldos { get; set; }
        public int  IdComuna { get; set; }
    }
}
