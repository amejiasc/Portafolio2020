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

        public RespuestaSubasta Leer(int idSubasta, string idSession)
        {
            var resultado = ListarActivas(idSession);
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
