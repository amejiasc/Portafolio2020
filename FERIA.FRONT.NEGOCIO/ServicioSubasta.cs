using FERIA.CLASES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.FRONT.NEGOCIO
{
    public class ServicioSubasta
    {
        ServicioApi servicio = new ServicioApi();

        public RespuestaDetalleSubasta Ofertar(DetalleSubasta detalle, string idSession) {
            RespuestaDetalleSubasta respuesta = new RespuestaDetalleSubasta();
            var respuestaApi = servicio.Post("api/Admin/Subasta/Ofertar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            },
            detalle);
            if (respuestaApi.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
                return msj;
            }
            return new RespuestaDetalleSubasta() { Exito = false, Mensaje = "No fue posible hacer la oferta", DetallleSubasta = new DetalleSubasta() };
        }

        public RespuestaSubastaListar ListarActivas(string idSession)
        {
            var resultado = Listar(idSession);
            if (resultado.Exito)
            {
                var listado = resultado.Subastas.Where(x => x.Estado.Equals(true) && x.FechaTermino >= DateTime.Now).ToList();
                if (!listado.Count.Equals(0))
                {
                    return new RespuestaSubastaListar() { Exito = true, Mensaje = "", Subastas = listado };
                }
                return new RespuestaSubastaListar() { Exito = false, Mensaje = "No existen subastas activas", Subastas = new List<Subasta>() };
            }
            else
                return new RespuestaSubastaListar() { Exito = false, Mensaje = resultado.Mensaje, Subastas = new List<Subasta>() };


        }
        /// <summary>
        /// Lee de la lista una subasta especifica por ID        
        /// </summary>
        /// <param name="idSubasta">Id Subasta a leer</param>
        /// <param name="idSession">Parametro de sesión</param>
        /// <param name="opcion">
        /// <list type="bullet">
        /// <item>
        /// <term>FILTRADO</term>
        /// <description>Filtra por Activas</description>
        /// </item>
        /// <item>
        /// <term>TODOS</term>
        /// <description>NO Tiene Filtros</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns></returns>
        public RespuestaSubasta Leer(int idSubasta, string idSession, string opcion = "FILTRADO")
        {
            RespuestaSubastaListar resultado;
            if (opcion.Equals("FILTRADO"))
            {
                resultado = ListarActivas(idSession);
            }
            else {
                resultado = Listar(idSession);
            }
            if (resultado.Exito)
            {
                var subasta = resultado.Subastas.Where(x => x.IdSubasta.Equals(idSubasta)).FirstOrDefault();
                if (subasta!=null)
                {
                    return new RespuestaSubasta() { Exito = true, Mensaje = "", Subasta = subasta };
                }
                return new RespuestaSubasta() { Exito = false, Mensaje = "No existe la subasta solicitada", Subasta = subasta };
            }
            else
                return new RespuestaSubasta() { Exito = false, Mensaje = resultado.Mensaje, Subasta = new Subasta() };


        }

        private RespuestaSubastaListar Listar(string idSession)
        {
            RespuestaSubastaListar respuesta = new RespuestaSubastaListar();
            var respuestaApi = servicio.Get("api/Admin/Subasta/Listar", new List<RestSharp.Parameter>()
            {
                new RestSharp.Parameter() { Name = "idSession", Value = idSession }
            });
            if (respuestaApi.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var msj = JsonConvert.DeserializeAnonymousType(respuestaApi.Content, respuesta);
                return msj;
            }
            return new RespuestaSubastaListar() { Exito = false, Mensaje = "No fue posible obtener información", Subastas = new List<Subasta>() };
        }



    }
}
