using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Clave { get; set; }
        public int Intentos { get; set; }
        public bool CambiaClave { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public bool Estado { get; set; }

        public string EstadoEdit { get; set; }
        public bool Activo { get; set; }

        public int IdPerfil { get; set; }

        public string CodigoPerfil { get; set; }
        public string NombrePerfil { get; set; }

        public int IdContratista { get; set; }

        [DataType(DataType.Password)]
        [Compare("Clave", ErrorMessage = "Confirmación de Clave no coincide")]
        [Required]
        [Display(Name = "Confirme su Clave")]
        public string ReClave { get; set; }
        public string SesionId { get; set; }

        public Usuario()
        {
            IdUsuario = 0;
            Nombre = string.Empty;
            Email = string.Empty;
            Clave = "";
            ReClave = "";
            Estado = true;
            EstadoEdit = "true";
            Activo = true;
            CambiaClave = false;
            FechaModificacion = DateTime.Now;
            FechaCreacion = DateTime.Now;
            IdContratista = 0;
            IdPerfil = 0;
        }
    }

    public class RespuestaUsuarioListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public RespuestaUsuarioListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Usuarios = new List<Usuario>();
        }
    }
    public class RespuestaUsuario
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
        public RespuestaUsuario()
        {
            Exito = true;
            Mensaje = string.Empty;
            Usuario = new Usuario();
        }
    }
}
