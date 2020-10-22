﻿using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FERIA.API.Controllers
{
    public class AdminController : ApiController
    {
        NEGOCIO.ServicioOrden servicioOrden;
        NEGOCIO.ServicioProceso servicioProceso;
        [HttpPost]
        [Route("api/Admin/{IdOrden}/Firmar")]
        public RespuestaFirmaOrden PostFirma(int IdOrden, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaFirmaOrden() { Exito = false, Mensaje = "No posee acceso valido"};
            }
            servicioOrden = new NEGOCIO.ServicioOrden(idSession);
            return servicioOrden.Firmar(IdOrden);
        }
        [HttpPost]
        [Route("api/Admin/{IdOrden}/Proceso")]
        public RespuestaProceso PostProceso(int IdOrden, [FromBody]Proceso proceso, string idSession)
        {

            if (string.IsNullOrEmpty(idSession))
            {
                return new RespuestaProceso() { Exito = false, Mensaje = "No posee acceso valido" };
            }
            servicioProceso = new NEGOCIO.ServicioProceso(idSession);
            return servicioProceso.Crear(proceso);
        }

    }
}
