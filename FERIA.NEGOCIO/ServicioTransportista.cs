using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioTransportista
    {
        STORE.ServicioCamion servicioCamion;
        public ServicioTransportista(string IdSession = "")
        {
            this.servicioCamion = new STORE.ServicioCamion(IdSession);
        }
        /// <summary>
        /// Lista de Camiones 
        /// </summary>
        /// <param name="idTransportista">Si va en 0 Lista todos los camiones</param>
        /// <returns></returns>
        public RespuestaCamionListar ListarCamiones(int idTransportista)
        {
            List<Camion> listados;
            if (idTransportista == 0)
            {
                listados = servicioCamion.Listar();
            }
            else
                listados = servicioCamion.Listar(idTransportista);

            if (listados == null)
            {
                return new RespuestaCamionListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los camiones" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaCamionListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaCamionListar() { Exito = true, Camiones = listados, Mensaje = "" };

        }
        /// <summary>
        /// Lee el camión por ID 
        /// </summary>
        /// <param name="idCamion"></param>
        /// <returns></returns>
        public RespuestaCamion LeerCamion(int idCamion)
        {
            var camion = servicioCamion.Leer(idCamion);

            if (camion == null)
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener el camion" };
            }
            if (camion.IdCamion == 0)
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaCamion() { Exito = true, Camion = camion, Mensaje = "" };

        }

        public RespuestaCamion CrearCamion(Camion camion)
        {   
            var respuesta = servicioCamion.Crear(camion);
            if (respuesta == null)
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de grabar el camión" };
            }
            if (respuesta.IdCamion.Equals(0))
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "Camión no fue creado" };
            }
            return new RespuestaCamion() { Exito = true, Mensaje = "Creación Exitosa", Camion = respuesta };
        }
        public RespuestaCamion ModificarCamion(Camion camion)
        {
            
            var respuesta = servicioCamion.Modificar(camion);
            if (respuesta == null)
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "Ha Ocurrido un error al momento de modificar el camión" };
            }
            if (respuesta.IdCamion.Equals(0))
            {
                return new RespuestaCamion() { Exito = false, Mensaje = "Camión no fue posible modificar" };
            }
            return new RespuestaCamion() { Exito = true, Mensaje = "Modificación Exitosa", Camion = respuesta };
        }

    }
}
