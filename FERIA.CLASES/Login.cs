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
    public class Recuperar
    {
        public string Rut { get; set; }
        public string Email { get; set; }
        public int TipoPerfil { get; set; }

        public string Servicio { get; set; }
        public Recuperar()
        {
            Servicio = "FRT";
        }
    }    
    public class RespuestaLogin
    {
        public bool Exito { get; set; }
        public MotivoNoExitoLogin Motivo { get; set; }
        public string Mensaje { get; set; }

        public Usuario Usuario { get; set; }

        public RespuestaLogin()
        {
            Exito = true;
            Motivo = MotivoNoExitoLogin.Exito;
            Mensaje = string.Empty;
        }

    }
    public enum MotivoNoExitoLogin
    {
        Exito = 0,
        ErrorNoControlado = 1,
        UsuarioNoExiste = 2,
        UsuarioClaveIncorrecta = 3,
        UsuarioNoVigente = 4,
        UsuarioEmailNoValido = 5,
        UsuarioRutInvalido = 6,
        UsuarioDebeCambiarClave = 7

    }
    public enum TipoPerfil
    {
        Administrador = 1,
        Productor = 2,
        Cliente_Externo = 3,
        Cliente_Interno = 4,
        Transportista = 5,
        Consultor = 6
    }
}
