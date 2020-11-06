using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioProceso
    {
        ServicioApi servicio = new ServicioApi();
        public RespuestaProcesoListar Listar(string idSession, string EstadoProceso)
        {
            var respuesta = Listar(idSession);
            if (respuesta.Exito)
            {
                if (respuesta.Procesos.Exists(x => x.EstadoProceso.Equals(EstadoProceso)))
                {
                    respuesta.Procesos = respuesta.Procesos.Where(x => x.EstadoProceso.Equals(EstadoProceso)).ToList();
                    if (respuesta.Procesos.Count.Equals(0)) {
                        return new RespuestaProcesoListar() { Exito = false, Mensaje = "No existen procesos en ese estado" };
                    }
                    if (EstadoProceso.Equals("PENDIENTE"))
                    {
                        respuesta.Procesos = respuesta.Procesos.Where(x => DateTime.Parse(x.FechaProceso.ToString("yyyy-MM-dd")) <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) && DateTime.Parse(x.FechaFinProceso.ToString("yyyy-MM-dd")) >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).ToList();
                        if (respuesta.Procesos.Count.Equals(0))
                        {
                            return new RespuestaProcesoListar() { Exito = false, Mensaje = "No existen procesos en ese estado" };
                        }
                    }
                    return respuesta;
                }
                else
                {
                    return new RespuestaProcesoListar() { Exito = false, Mensaje = "No existen procesos en ese estado" };
                }
            }
            else
                return respuesta;

            
        }
        public RespuestaProcesoListar Listar(string idSession)
        {
            RespuestaProcesoListar respuesta = new RespuestaProcesoListar();
            var respuestaApi = servicio.Get("api/admin/proceso/listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            if (respuestaApi.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
                return msj;
            }
            return new RespuestaProcesoListar() { Exito = false, Mensaje = "No hubo respuesta desde la api" };

        }
        public RespuestaProceso Leer(int idProceso, string idSession)
        {
            var respuesta = Listar(idSession);
            if (respuesta.Exito)
            {
                if (respuesta.Procesos.Exists(x => x.IdProceso.Equals(idProceso)))
                {
                    var proceso = respuesta.Procesos.FirstOrDefault(x => x.IdProceso.Equals(idProceso));
                    return new RespuestaProceso() { Exito = true, Proceso = proceso };
                }
                else
                {
                    return new RespuestaProceso() { Exito = false, Mensaje = "No existe proceso solicitado" };
                }
            }
            else
                return new RespuestaProceso() { Mensaje = "No existe el proceso", Exito=false };


        }

    }
}
