using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioUtil
    {
        STORE.ServicioUtil servicioUtil;
        public ServicioUtil(string idSession = "")
        {
            this.servicioUtil = new STORE.ServicioUtil(idSession);
        }
        public List<Region> Regiones()
        {
            return servicioUtil.ListarRegiones();
        }
        public List<Comuna> Comunas()
        {
            return servicioUtil.ListarComunas();
        }
        public List<Categoria> Categorias()
        {
            return servicioUtil.ListarCategorias();
        }
        public RespuestaCategoria CrearCategoria(Categoria categoria) 
        {
            if (string.IsNullOrEmpty(categoria.NombreCategoria)) {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Nombre Categoría no puede ir vacío" };
            }
            if (string.IsNullOrEmpty(categoria.NombreIngles))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Nombre Categoría en ingles no puede ir vacío" };
            }
            if (string.IsNullOrEmpty(categoria.Descripcion))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Descripción no puede ir vacío" };
            }
            var retorno = servicioUtil.CrearCategoria(categoria);
            if (retorno.Equals(0)) {
                    return new RespuestaCategoria() { Exito = false, Mensaje = "Ha ocurrido un error al crear una categoria" };
            }
            var categoriacreada = servicioUtil.ListarCategorias().OrderByDescending(x=>x.IdCategoria).FirstOrDefault(x=>x.NombreCategoria.Contains(categoria.NombreCategoria));
            return new RespuestaCategoria() { Exito = true, Mensaje = "Creación Exitosa", Categoria= categoriacreada };
        }

        public RespuestaCategoria ModificarCategoria(Categoria categoria)
        {
            if (string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Nombre Categoría no puede ir vacío" };
            }
            if (string.IsNullOrEmpty(categoria.NombreIngles))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Nombre Categoría en ingles no puede ir vacío" };
            }
            if (string.IsNullOrEmpty(categoria.Descripcion))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Descripción no puede ir vacío" };
            }
            var retorno = servicioUtil.ModificarCategoria(categoria);
            if (retorno.Equals(0))
            {
                return new RespuestaCategoria() { Exito = false, Mensaje = "Ha ocurrido un error al crear una categoria" };
            }
            return new RespuestaCategoria() { Exito = true, Mensaje = "Modificación Exitosa", Categoria = categoria};
        }

        public List<Perfil> Perfiles()
        {
            return servicioUtil.ListarPerfiles ();
        }
        public List<string> EstadosOrdenes() 
        {
            return "ANULADO,CERRADO,PENDIENTE,VIGENTE".ToString().Split(',').ToList();        
        }
        public List<string> EstadosProcesos()
        {
            return "ANULADO,CERRADO,EN_PROCESO,PENDIENTE,PUBLICADO".ToString().Split(',').ToList();
        }
        public List<string> EstadosOfertas()
        {
            return "INGRESADA,ANULADA,GANADA".ToString().Split(',').ToList();
        }
        public List<string> EstadosDetalleSubasta()
        {
            return "EN_PROCESO,CANCELADA,GANADA".ToString().Split(',').ToList();
        }
        public List<string> TipoVenta()
        {
            return "Extranjera,Local".ToString().Split(',').ToList();
        }

    }
}
