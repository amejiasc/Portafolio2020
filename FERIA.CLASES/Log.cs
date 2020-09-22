using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Log
    {
        public string IdSession { get; set; }
        public string Servicio { get; set; }
        public string SubServicio { get; set; }
        public int Codigo { get; set; }
        public string Entrada { get; set; }
        public string Salida { get; set; }
        public string Estado { get; set; }

        public Log()
        {
            IdSession = string.Empty;
        }
    }
}
