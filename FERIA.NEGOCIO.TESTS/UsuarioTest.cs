using FERIA.NEGOCIO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FERIA.CLASES;
using System.Net.Http;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace FERIA.NEGOCIO.Tests
{
    [TestClass()]
    public class UsuarioTest
    {
        [TestMethod()]
        public void LoginTest()
        {
            RespuestaLogin respuestaLogin = new RespuestaLogin();
            respuestaLogin.Motivo = MotivoNoExitoLogin.ErrorNoControlado;
            switch (respuestaLogin.Motivo)
            {
                case MotivoNoExitoLogin.UsuarioClaveIncorrecta:
                    break;
                case MotivoNoExitoLogin.UsuarioNoExiste:
                    break;
                default:
                    Assert.Fail();
                    break;
            }


        }
        [TestMethod()]
        public void Login_RutInvalidoTest()
        {
            string rut = "15538372-1";
            string clave = "12345a";
            HttpRequestMessage request = new HttpRequestMessage();
            var respuesta = new NEGOCIO.ServicioLogin().Login(rut, clave, request);
            RespuestaLogin respuestaLogin = new RespuestaLogin();
            var msj = JsonConvert.DeserializeAnonymousType(respuesta.ToString(), respuestaLogin);
            Assert.AreEqual(msj.Exito, false, msj.Mensaje);

        }
    }
}
