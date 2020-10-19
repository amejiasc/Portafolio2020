using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.CLASES
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int ValorUnitario { get; set; }
        public string TipoVenta { get; set; }
        public int Calidad { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdCategoria { get; set; }

        public Categoria Categoria { get; set; }

        public int IdUsuario { get; set; }

    }

    public class RespuestaProductoListar
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Producto> Productos { get; set; }
        public RespuestaProductoListar()
        {
            Exito = true;
            Mensaje = string.Empty;
            Productos = new List<Producto>();
        }
    }
    public class RespuestaProducto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public Producto Producto { get; set; }
        public RespuestaProducto()
        {
            Exito = true;
            Mensaje = string.Empty;
            Producto = new Producto();
        }
    }
    public enum TipoVenta
    {
        Local = 1,
        Extranjera = 2
    }
}
