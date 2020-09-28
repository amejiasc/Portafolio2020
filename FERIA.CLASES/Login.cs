using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Login
    {
        public string Rut { get; set; }
        public string Clave { get; set; }
        public int TipoPerfil { get; set; }

    }
    public class RespuestaLogin
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public RespuestaLogin()
        {
            Exito = true;
            Mensaje = string.Empty;
            Usuario = new Usuario();
            
        }
    }
    public enum TipoPerfil
    {
        Administrador = 1,
        Productor = 2
    }
}
