using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Region
    {
        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public Region()
        {
            this.IdRegion = 0;
            this.NombreRegion = "";
        }

    }
    public class Comuna
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public int CodigoComuna { get; set; }
        public string NombreComuna { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreRegion { get; set; }
        public Comuna()
        {
            this.CodigoComuna = 0;
            this.IdRegion = 0;
            this.IdComuna = 0;
            this.NombreComuna = "";
            this.NombreCiudad = "";
            this.NombreRegion = "";
        }

    }
    public class Perfil
    {

        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
        public string CodigoPerfil { get; set; }
        public string ServicioPerfil { get; set; }
        public bool Estado { get; set; }
        public Perfil()
        {
            this.IdPerfil = 0;
            this.NombrePerfil = string.Empty;
            this.CodigoPerfil = string.Empty;
        }
    }

    public class Codigo
    {
        public int IdCodigo { get; set; }
        public string Tipo { get; set; }
        public int Valor { get; set; }
        public string Glosa { get; set; }

        public string CodigoTxt { get; set; }
        public bool Estado { get; set; }

    }

    public class Categoria {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreIngles { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionIngles { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuario { get; set; }
    }    
    public class RespuestaCategoria
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Categoria Categoria { get; set; }
        public RespuestaCategoria()
        {
            Exito = true;
            Mensaje = string.Empty;
            Categoria = new Categoria();
        }
    }
    public class RespuestaCategoriaListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Categoria> Categorias { get; set; }
        public RespuestaCategoriaListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Categorias = new List<Categoria>();
        }
    }

}
