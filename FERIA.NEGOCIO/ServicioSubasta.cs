using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioSubasta
    {
        STORE.ServicioSubasta servicioSubasta;
        ServicioCorreo servicioCorreo = new ServicioCorreo();
        public ServicioSubasta(string IdSession = "")
        {
            this.servicioSubasta = new STORE.ServicioSubasta(IdSession);
        }        
        public RespuestaSubasta Crear(Subasta subasta) 
        {
            if (subasta.FechaSubasta >= subasta.FechaTermino) {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Fecha de inicio subasta no puede ser mayor a la término" };
            }

            var resultado = servicioSubasta.Listar(subasta.IdProceso).FirstOrDefault(x=>x.Estado.Equals(true));
            if (resultado != null)
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Ya se ha realizado una subasta para este proceso" };
            }
            var respuesta = servicioSubasta.Crear(subasta);
            if (respuesta == 0)
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar la subasta" };
            }
            return new RespuestaSubasta() { Exito = true, Mensaje = "Creación Exitosa", Subasta = new Subasta() };
        }
        public RespuestaSubasta Modificar(Subasta subasta)
        {
            if (subasta.FechaSubasta >= subasta.FechaTermino)
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Fecha de inicio subasta no puede ser mayor a la término" };
            }

            var respuesta = servicioSubasta.Modificar(subasta);
            if (respuesta == 0)
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar la subasta" };
            }
            return new RespuestaSubasta() { Exito = true, Mensaje = "Modificación Exitosa", Subasta = new Subasta() };
        }

        public RespuestaSubasta Leer(int idSubasta)
        {
            var resultado = servicioSubasta.Leer(idSubasta);
            if (resultado == null)
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "Ha ocurrido un error validando la subasta" };
            }
            if (resultado.IdSubasta.Equals(0))
            {
                return new RespuestaSubasta() { Exito = false, Mensaje = "No existe subasta solicitada" };
            }
            return new RespuestaSubasta() { Exito = true, Mensaje = "", Subasta = resultado };
        }

        public RespuestaSubastaListar Listar(int idProceso)
        {
            var respuesta = servicioSubasta.Listar(idProceso);
            if (respuesta == null)
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de traer las subastas" };
            if (respuesta.Count.Equals(0))
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No hay subastas para listar" };

            return new RespuestaSubastaListar() { Subastas = respuesta };

        }
        public RespuestaSubastaListar Listar()
        {
            var respuesta = servicioSubasta.Listar();
            if (respuesta == null)
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de traer las subastas" };
            if (respuesta.Count.Equals(0))
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No hay subastas para listar" };

            return new RespuestaSubastaListar() { Subastas = respuesta };

        }

    }
}
