using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioUsuario
    {
        STORE.ServicioUsuario servicioUsuario;
        ServicioCorreo servicioCorreo;


        public ServicioUsuario(string IdSession = "")
        {
            this.servicioUsuario = new STORE.ServicioUsuario(IdSession);
            this.servicioCorreo = new ServicioCorreo();

        }
        public RespuestaUsuario Desactivar(int idUsuario)
        {
            var usuario = servicioUsuario.Desactivar(idUsuario);
            if (usuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al desactivar el usuario" };
            }
            return new RespuestaUsuario() { Exito = true, Mensaje = "Desactivación del usuario fue satisfactoria" };
        }

        public RespuestaUsuario Reiniciar(int idUsuario)
        {
            string NewClave = Funciones.Varias.RandomPassword();
            var usuario1 = servicioUsuario.Leer(idUsuario);
            if (usuario1 == null)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al recuperar al usuario" };
            }
            if (usuario1.IdUsuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Usuario no existe" };
            }
            var usuario = servicioUsuario.ReiniciarClave(idUsuario, NewClave);
            if (usuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al desactivar el usuario" };
            }
            string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
            readText = readText.Replace("{@TituloPagina}", "Reinicio de Clave");
            readText = readText.Replace("{@Titulo}", "Se ha reiniciado su Clave");
            readText = readText.Replace("{@SubTitulo}", "Envío de Clave Provisoría");
            readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + usuario1.Nombre + " " + usuario1.Apellido + "</b>:<br />A continuación enviamos clave provisoría que debe ser utilizada en el portal. Posterior a eso, se solicitará una nueva clave para que pueda recordarla.<br />Su clave: <b>" + NewClave + "</b>");

            servicioCorreo.Asunto = "[RESERVA] - Reinicio de Clave";
            servicioCorreo.Enviar(readText, usuario1.Email);

            return new RespuestaUsuario() { Exito = true, Mensaje = "Se ha reiniciado la clave del usuario" };

        }
        public RespuestaUsuario Activar(int idUsuario)
        {
            var usuario = servicioUsuario.Activar(idUsuario);

            if (usuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al activar el usuario" };
            }
            return new RespuestaUsuario() { Exito = true, Mensaje = "Activación del usuario fue satisfactoria" };
        }
        public RespuestaUsuario Leer(int idUsuario)
        {
            var usuario = servicioUsuario.Leer(idUsuario);

            if (usuario == null)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al recuperar al usuario" };
            }
            if (usuario.IdUsuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Usuario no existe" };
            }
            return new RespuestaUsuario() { Exito = true, Usuario = usuario };
        }
        public RespuestaUsuario Leer(string rutUsuario, string servicio = "BAK")
        {
            if (!Funciones.Varias.ValidarRut(rutUsuario))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado no es válido" };
            }
            else
            {
                rutUsuario = Funciones.Varias.FormatearRut(rutUsuario).Replace(".", "");
            }

            var usuario = servicioUsuario.Listar(0, servicio).FirstOrDefault(x => x.Rut.Equals(rutUsuario));
            if (usuario == null)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Hubo un problema al recuperar al usuario" };
            }
            if (usuario.IdUsuario == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Usuario no existe" };
            }
            return new RespuestaUsuario() { Exito = true, Usuario = usuario };
        }

        public RespuestaUsuario CrearUsuarioControl(CLASES.Usuario usuario, string tipo)
        {

            string perfilACrear = "CHOFE";
            if (tipo.ToLower().Equals("auxiliar"))
            {
                perfilACrear = "AUXIL";
            }
            ServicioUtil servicioUtil = new ServicioUtil();
            usuario.IdPerfil = servicioUtil.ListarPerfiles().FirstOrDefault(x => x.CodigoPerfil.Equals(perfilACrear)).IdPerfil;
            if (!Funciones.Varias.ValidarRut(usuario.Rut))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado no es válido" };
            }
            else
            {
                usuario.Rut = Funciones.Varias.FormatearRut(usuario.Rut).Replace(".", "");
            }
            if (!Funciones.Varias.ValidarEmail(usuario.Email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }
            var usuarios = servicioUsuario.Listar(0, "FRT");
            if (usuarios.Exists(x => x.CodigoPerfil.Equals(perfilACrear) && x.Email.ToLower().Equals(usuario.Email.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado ya existe. Debe ser un email único" };
            }
            if (usuarios.Exists(x => x.CodigoPerfil.Equals(perfilACrear) && x.Rut.ToLower().Equals(usuario.Rut.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado ya existe." };
            }
            var id = servicioUsuario.CrearFront(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }
            else
            {
                var usuarioCreado = servicioUsuario.LeerByRut(usuario.Rut);
                if (usuarioCreado.IdUsuario != 0)
                {
                    if (servicioUsuario.AsociarPerfil(usuarioCreado.IdUsuario, usuario.IdPerfil, usuario.IdContratista) == 1)
                    {
                        usuarioCreado = servicioUsuario.Leer(usuarioCreado.IdUsuario);
                    }
                    else
                    {
                        servicioUsuario.Eliminar(usuarioCreado.IdUsuario);
                        return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                    }
                }
                else
                {
                    return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                }
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
                readText = readText.Replace("{@TituloPagina}", "Creación de Usuario " + tipo);
                readText = readText.Replace("{@Titulo}", "Nuevo Registro de Usuario " + tipo);
                readText = readText.Replace("{@SubTitulo}", "Envío de Clave");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + usuarioCreado.Nombre + " " + usuarioCreado.Apellido + "</b>:<br />Se ha creado una cuenta nueva. A continuación enviamos copia de la clave que has creado para ser utilizada en el portal.<br />Su clave: <b>" + usuario.Clave + "</b>");

                servicioCorreo.Asunto = "[REGISTRO] - Nuevo Usuario " + tipo;
                servicioCorreo.Enviar(readText, usuarioCreado.Email);
                return new RespuestaUsuario() { Exito = true, Usuario = usuarioCreado, Mensaje = "Usuario " + tipo + " creado Satisfactoriamente" };
            }
        }

        public RespuestaUsuario CrearUsuarioPasajero(CLASES.Usuario usuario)
        {
            ServicioUtil servicioUtil = new ServicioUtil();
            usuario.IdPerfil = servicioUtil.ListarPerfiles().FirstOrDefault(x => x.CodigoPerfil.Equals("PERSO")).IdPerfil;
            if (!Funciones.Varias.ValidarRut(usuario.Rut))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado no es válido" };
            }
            else
            {
                usuario.Rut = Funciones.Varias.FormatearRut(usuario.Rut).Replace(".", "");
            }
            if (!Funciones.Varias.ValidarEmail(usuario.Email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }
            var usuarios = servicioUsuario.Listar(0, "FRT");
            if (usuarios.Exists(x => x.CodigoPerfil.Equals("PERSO") && x.Email.ToLower().Equals(usuario.Email.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado ya existe. Debe ser un email único" };
            }
            if (usuarios.Exists(x => x.CodigoPerfil.Equals("PERSO") && x.Rut.ToLower().Equals(usuario.Rut.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado ya existe." };
            }

            var id = servicioUsuario.CrearFront(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }
            else
            {
                var usuarioCreado = servicioUsuario.Leer(usuario.Email);
                if (usuarioCreado.IdUsuario != 0)
                {
                    if (servicioUsuario.AsociarPerfil(usuarioCreado.IdUsuario, usuario.IdPerfil, usuario.IdContratista) == 1)
                    {
                        usuarioCreado = servicioUsuario.Leer(usuarioCreado.IdUsuario);
                    }
                    else
                    {
                        servicioUsuario.Eliminar(usuarioCreado.IdUsuario);
                        return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                    }
                }
                else
                {
                    return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                }
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
                readText = readText.Replace("{@TituloPagina}", "Creación de Pasajero");
                readText = readText.Replace("{@Titulo}", "Nuevo Registro de Pasajero");
                readText = readText.Replace("{@SubTitulo}", "Envío de Clave");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + usuarioCreado.Nombre + " " + usuarioCreado.Apellido + "</b>:<br />Se ha creado una cuenta nueva. A continuación enviamos copia de la clave que has creado para ser utilizada en el portal.<br />Su clave: <b>" + usuario.Clave + "</b>");

                servicioCorreo.Asunto = "[REGISTRO] - Nuevo Pasajero";
                servicioCorreo.Enviar(readText, usuarioCreado.Email);
                return new RespuestaUsuario() { Exito = true, Usuario = usuarioCreado, Mensaje = "Pasajero creado Satisfactoriamente" };
            }
        }

        public RespuestaUsuario Crear(CLASES.Usuario usuario)
        {

            string NewClave = Funciones.Varias.RandomPassword();
            usuario.Clave = NewClave;
            if (!Funciones.Varias.ValidarRut(usuario.Rut))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado no es válido" };
            }
            else
            {
                usuario.Rut = Funciones.Varias.FormatearRut(usuario.Rut).Replace(".", "");
            }
            if (!Funciones.Varias.ValidarEmail(usuario.Email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }
            var usuarios = servicioUsuario.Listar(0);
            if (usuarios.Exists(x => x.Email.ToLower().Equals(usuario.Email.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado ya existe. Debe ser un email único" };
            }
            if (servicioUsuario.Listar(0).Exists(x => x.Rut.ToLower().Equals(usuario.Rut.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Rut ingresado ya existe." };
            }

            var id = servicioUsuario.Crear(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }
            else
            {
                var usuarioCreado = servicioUsuario.Leer(usuario.Email);
                if (usuarioCreado.IdUsuario != 0)
                {
                    if (servicioUsuario.AsociarPerfil(usuarioCreado.IdUsuario, usuario.IdPerfil, usuario.IdContratista) == 1)
                    {
                        usuarioCreado = servicioUsuario.Leer(usuarioCreado.IdUsuario);
                    }
                    else
                    {
                        servicioUsuario.Eliminar(usuarioCreado.IdUsuario);
                        return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                    }
                }
                else
                {
                    return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha grabado el usuario." };
                }
                string readText = Funciones.Varias.GetHtmlPlantilla(Funciones.Varias.PlantillaDisponible.Recuperar);
                readText = readText.Replace("{@TituloPagina}", "Creación de Usuario");
                readText = readText.Replace("{@Titulo}", "Nuevo Registro de Usuario");
                readText = readText.Replace("{@SubTitulo}", "Envío de Clave Provisoría");
                readText = readText.Replace("{@Contenido}", "Estimado(a) <b>" + usuarioCreado.Nombre + " " + usuarioCreado.Apellido + "</b>:<br />Se ha creado una cuenta nueva. A continuación enviamos clave provisoría que debe ser utilizada en el portal. Posterior a eso, se solicitará una nueva clave para que pueda recordarla.<br />Su clave: <b>" + NewClave + "</b>");

                servicioCorreo.Asunto = "[REGISTRO] - Nuevo Usuario";
                servicioCorreo.Enviar(readText, usuarioCreado.Email);
                return new RespuestaUsuario() { Exito = true, Usuario = usuarioCreado, Mensaje = "Usuario creado Satisfactoriamente" };

            }
        }

        public RespuestaUsuario Modificar(CLASES.Usuario usuario)
        {

            if (servicioUsuario.Listar(0).Exists(x => !x.IdUsuario.Equals(usuario.IdUsuario) && x.Email.ToLower().Equals(usuario.Email.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado ya existe en la base de datos" };
            }
            if (!Funciones.Varias.ValidarEmail(usuario.Email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }
            if (usuario.IdPerfil != 0)
            {
                if (servicioUsuario.AsociarPerfil(usuario.IdUsuario, usuario.IdPerfil, usuario.IdContratista) == 0)
                {
                    return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha modificado el perfil del usuario." };
                }
            }

            var id = servicioUsuario.Modificar(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }
            var usuarioCreado = servicioUsuario.Leer(usuario.IdUsuario);
            return new RespuestaUsuario() { Exito = true, Mensaje = "Usuario modificado Satisfactoriamente", Usuario = usuarioCreado };
        }
        public RespuestaUsuario ModificarFront(CLASES.Usuario usuario)
        {

            if (!Funciones.Varias.ValidarEmail(usuario.Email))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado no es válido" };
            }
            if (servicioUsuario.Listar(0, "FRT").Exists(x => !x.IdUsuario.Equals(usuario.IdUsuario) && x.Email.ToLower().Equals(usuario.Email.ToLower())))
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Email ingresado ya existe en la base de datos" };
            }
            if (usuario.IdPerfil != 0)
            {
                if (servicioUsuario.AsociarPerfil(usuario.IdUsuario, usuario.IdPerfil, usuario.IdContratista) == 0)
                {
                    return new RespuestaUsuario() { Exito = false, Mensaje = "No se ha modificado el perfil del usuario." };
                }
            }

            var id = servicioUsuario.Modificar(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }
            var usuarioCreado = servicioUsuario.Leer(usuario.IdUsuario);
            return new RespuestaUsuario() { Exito = true, Mensaje = "Usuario modificado Satisfactoriamente", Usuario = usuarioCreado };
        }
        public RespuestaUsuario ModificarClave(CLASES.Usuario usuario)
        {

            var id = servicioUsuario.ModificarClave(usuario);
            if (id == 0)
            {
                return new RespuestaUsuario() { Exito = false, Mensaje = "Ha ocurrido un error al momento de grabar al usuario" };
            }

            return new RespuestaUsuario() { Exito = true };
        }
        public RespuestaUsuarioListar ListarUsuarios(int idPerfil)
        {
            var listados = servicioUsuario.Listar(idPerfil);

            if (listados == null)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los usuarios" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaUsuarioListar() { Exito = true, Usuarios = listados, Mensaje = "" };

        }
        public RespuestaUsuarioListar ListarUsuariosFront(int idPerfil)
        {
            var listados = servicioUsuario.Listar(idPerfil, "FRT");

            if (listados == null)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los usuarios" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaUsuarioListar() { Exito = true, Usuarios = listados, Mensaje = "" };

        }
        public RespuestaUsuarioListar ListarUsuariosFront(Usuario usuario)
        {
            var listados = servicioUsuario.Listar(usuario.IdPerfil, "FRT")
                           .Where(x => x.Nombre.Contains(usuario.Nombre) || string.IsNullOrEmpty(usuario.Nombre))
                           .Where(x => x.Apellido.Contains(usuario.Apellido) || string.IsNullOrEmpty(usuario.Apellido))
                           .Where(x => x.Email.Contains(usuario.Email) || string.IsNullOrEmpty(usuario.Email))
                           .Where(x => x.Rut.Contains(usuario.Rut) || string.IsNullOrEmpty(usuario.Rut)).ToList();
            if (!usuario.EstadoEdit.Equals("-1"))
            {
                listados = listados.Where(x => x.Estado.Equals(usuario.EstadoEdit.Equals("1"))).ToList();
            }

            if (listados == null)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los usuarios" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaUsuarioListar() { Exito = true, Usuarios = listados, Mensaje = "" };

        }
        public RespuestaUsuarioListar ListarUsuarios(Usuario usuario)
        {
            var listados = servicioUsuario.Listar(usuario.IdPerfil)
                           .Where(x => x.Nombre.Contains(usuario.Nombre) || string.IsNullOrEmpty(usuario.Nombre))
                           .Where(x => x.Apellido.Contains(usuario.Apellido) || string.IsNullOrEmpty(usuario.Apellido))
                           .Where(x => x.Email.Contains(usuario.Email) || string.IsNullOrEmpty(usuario.Email))
                           .Where(x => x.Rut.Contains(usuario.Rut) || string.IsNullOrEmpty(usuario.Rut)).ToList();
            if (!usuario.EstadoEdit.Equals("-1"))
            {
                listados = listados.Where(x => x.Estado.Equals(usuario.EstadoEdit.Equals("1"))).ToList();
            }

            if (listados == null)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "Ha ocurrido un error al momento de obtener los usuarios" };
            }
            if (listados.Count == 0)
            {
                return new RespuestaUsuarioListar() { Exito = false, Mensaje = "No existen datos" };
            }

            return new RespuestaUsuarioListar() { Exito = true, Usuarios = listados, Mensaje = "" };

        }


    }
}
