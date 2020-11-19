using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Camion
    {        
        public int PesoMaximo { get; set; }
        public bool Refrigera { get; set; }
        public int IdUsuario { get; set; }
        public int IdCamion { get; set; }

    }
    public class RespuestaCamion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Camion Camion { get; set; }
        public RespuestaCamion()
        {
            Exito = true;
            Mensaje = string.Empty;
        }
    }
    public class RespuestaCamionListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Camion> Camiones { get; set; }
        public RespuestaCamionListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Camiones = new List<Camion>();
        }
    }
}
