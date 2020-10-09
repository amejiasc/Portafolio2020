using FERIA.CLASES;
using FERIA.FRONT.NEGOCIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace FERIA.FRONT.Controllers
{
    public class HomeController : Controller
    {
        ServicioLogin servicioLogin;
        ServicioUsuario servicioUsuario;
        public HomeController()
        {
            servicioLogin = new ServicioLogin();
            servicioUsuario = new ServicioUsuario();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(bool exito = false)
        {
            if (exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha creado el usuario satisfactoriamente";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(string rut, string clave, int perfil)
        {
            var Respuesta = servicioLogin.Login(new CLASES.Login() { Clave=clave, Rut=rut, TipoPerfil = perfil  });
            if (Respuesta.Exito && Respuesta.Motivo == MotivoNoExitoLogin.Exito)
            {
                Helper.Autenticacion.Login(Respuesta.Usuario);
                return RedirectToAction("Index", "Sistema");

            }
            else if (Respuesta.Exito && Respuesta.Motivo == MotivoNoExitoLogin.UsuarioDebeCambiarClave)
            {
                return RedirectToAction("CambiarClave", "Home", Respuesta.Usuario);
            }
            ViewBag.Error = true;
            ViewBag.Mensaje = Respuesta.Mensaje;
            return View("Index");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(bool cerrarSesion = false)
        {
            ViewBag.CerrarSesion = cerrarSesion;
            return View("Index");
        }
        [HttpPost]
        public ActionResult Recuperar(string Rut, string Email)
        {
            var Respuesta = servicioLogin.Recuperar(new CLASES.Recuperar() { Email=Email, Rut= Rut, Servicio="FRT", TipoPerfil=0  });
            if (Respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha enviado la clave nueva a su Email.";
                return View("Index");

            }
            ViewBag.Error = true;
            ViewBag.Mensaje = Respuesta.Mensaje;
            return View("Index");
        }
        public ActionResult CambiarClave(Usuario usuario)
        {
            Session["usuario"] = usuario;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult CambiarClave(int IdUsuario, string ClaveProvisoria, string Clave, string ReClave)
        {
            var usuario = (Usuario)Session["usuario"];
            var respuesta = servicioLogin.CambiarClave(IdUsuario, ClaveProvisoria, Clave, ReClave, usuario.SesionId);
            if (respuesta.Exito)
            {
                ViewBag.Exito = true;
                ViewBag.Mensaje = "Se ha modificado la clave correctamente";
                return View("Index");
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = respuesta.Mensaje;
                return View("CambiarClave", Session["usuario"]);
            }

        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost ]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Registro(FRONT.Models.Usuario usuario)
        {
            CLASES.Usuario usuarioObj;
            switch (usuario.IdPerfil)
            {
                case (int)TipoPerfil.Productor:
                    usuarioObj = new CLASES.Productor()
                    {
                        Activo = true,
                        Apellido = usuario.Apellido,
                        CambiaClave  = false,
                        Clave = usuario.Clave,
                        Direccion = usuario.Direccion,
                        Email = usuario.Email,
                        Estado = true,
                        IdComuna  = usuario.IdComuna,
                        IdPerfil  = usuario.IdPerfil,
                        Intentos=0,
                        Nombre = usuario.Nombre ,
                        NombreProductor  = usuario.NombreCliente,
                        Rut = usuario.Rut,
                        RutProductor = usuario.RutCliente, 
                        Telefono = usuario.Telefono 
                    };
                    break;
                case (int)TipoPerfil.Cliente_Externo:
                    usuarioObj = new CLASES.ClienteExterno()
                    {
                        Activo = true,
                        Apellido = usuario.Apellido,
                        CambiaClave = false,
                        Ciudad  = usuario.Ciudad,
                        Clave = usuario.Clave,
                        Direccion = usuario.Direccion,
                        Email = usuario.Email,
                        Estado = true,                        
                        IdPerfil = usuario.IdPerfil,
                        Intentos = 0,
                        Nombre = usuario.Nombre,
                        NombreEmpresa  = usuario.NombreCliente,
                        Pais = usuario.Pais,
                        Rut = usuario.Rut,                        
                        Telefono = usuario.Telefono
                    };
                    break;
                case (int)TipoPerfil.Cliente_Interno:
                    usuarioObj = new CLASES.ClienteInterno()
                    {
                        Activo = true,
                        Apellido = usuario.Apellido,
                        CambiaClave = false,
                        Clave = usuario.Clave,
                        Direccion = usuario.Direccion,
                        Email = usuario.Email,
                        Estado = true,
                        IdComuna = usuario.IdComuna,
                        IdPerfil = usuario.IdPerfil,
                        Intentos = 0,
                        Nombre = usuario.Nombre,
                        NombreCliente  = usuario.NombreCliente,
                        Rut = usuario.Rut,
                        RutCliente = usuario.RutCliente,
                        Telefono = usuario.Telefono
                    };
                    break;
                case (int)TipoPerfil.Transportista:
                    usuarioObj = new CLASES.Transportista()
                    {
                        Activo = true,
                        Apellido = usuario.Apellido,
                        CambiaClave = false,
                        Clave = usuario.Clave,
                        Direccion = usuario.Direccion,
                        Email = usuario.Email,
                        Estado = true,
                        IdComuna = usuario.IdComuna,
                        IdPerfil = usuario.IdPerfil,
                        Intentos = 0,
                        Nombre = usuario.Nombre,
                        NombreTransportista  = usuario.NombreCliente,
                        Rut = usuario.Rut,
                        RutTransportista = usuario.RutCliente,
                        Telefono = usuario.Telefono
                    };
                    break;
                default:
                    usuarioObj = new CLASES.Usuario()
                    {
                        Activo = true,
                        Apellido = usuario.Apellido,
                        CambiaClave = false,
                        Clave = usuario.Clave,
                        Direccion = usuario.Direccion,
                        Email = usuario.Email,
                        Estado = true,
                        IdPerfil = usuario.IdPerfil,
                        Intentos = 0,
                        Nombre = usuario.Nombre,
                        Rut = usuario.Rut,
                        Telefono = usuario.Telefono
                    };
                    break;
            }

            var respuesta = servicioUsuario.Crear(JObject.FromObject(usuarioObj), "FRT", usuario.IdPerfil);
            if (respuesta.Exito)
            {
                return RedirectToAction("Index", "Home", new { exito=true });
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = respuesta.Mensaje;
                return View(usuario);
            }
        }


    }
}