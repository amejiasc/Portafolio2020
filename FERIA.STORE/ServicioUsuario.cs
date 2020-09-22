using FERIA.CLASES;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FERIA.STORE
{
    public class ServicioUsuario
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Usuario";
            }
        }
        private int Codigo
        {
            get
            {
                return 1000;

            }
        }
        public string IdSession { get; set; }
        public ServicioUsuario(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public int Activar(int idUsuario)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                //string sql = "UPDATE Usuario SET Estado=1 WHERE idUsuario={0} ";
                //sql = string.Format(sql, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log() { IdSession = this.IdSession, Servicio = this.Servicio, SubServicio = m.Name, Codigo = this.Codigo + 1, Estado = "OK", Entrada = js.Serialize(new { idUsuario }), Salida = js.Serialize(new { Respuesta = "OK" }) });
                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log() { IdSession = this.IdSession, Servicio = this.Servicio, SubServicio = m.Name, Codigo = this.Codigo + 1, Estado = "ERROR", Entrada = js.Serialize(new { idUsuario }), Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException }) });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public int Desactivar(int idUsuario)
        {
            try
            {
                //string sql = "UPDATE Usuario SET Estado=0 WHERE idUsuario={0} ";
                //sql = string.Format(sql, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();
                servicioLogTrace.Grabar(new Log() { IdSession = this.IdSession, Servicio = this.Servicio, SubServicio = "Desactivar", Codigo = this.Codigo + 2, Estado = "OK", Entrada = js.Serialize(new { idUsuario }), Salida = js.Serialize(new { Respuesta = "OK" }) });
                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log() { IdSession = this.IdSession, Servicio = this.Servicio, SubServicio = "Desactivar", Codigo = this.Codigo + 2, Estado = "ERROR", Entrada = js.Serialize(new { idUsuario }), Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException }) });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public int Eliminar(int idUsuario)
        {
            try
            {
                //string sql = "DELETE FROM Usuario WHERE idUsuario={0} ";
                //sql = string.Format(sql, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Eliminar",
                    Codigo = this.Codigo + 3,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario }),
                    Salida = js.Serialize(Leer(idUsuario))
                });
                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Eliminar",
                    Codigo = this.Codigo + 3,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { idUsuario }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public int AsociarPerfil(int idUsuario, int idPerfil, int idContratista)
        {
            try
            {
                //string sql = "IF NOT EXISTS(SELECT TOP 1 1 FROM UsuarioPerfil WHERE IdUsuario={0}) BEGIN ";
                //sql = string.Concat(sql, " INSERT INTO UsuarioPerfil (IdUsuario, IdPerfil, Estado, idContratista)");
                //sql = string.Concat(sql, " SELECT {0}, {1}, 1, {2} ");
                //sql = string.Concat(sql, " End ");
                //sql = string.Concat(sql, " ELSE ");
                //sql = string.Concat(sql, " BEGIN ");
                //sql = string.Concat(sql, " UPDATE UsuarioPerfil SET IdPerfil={1}, idContratista={2} where IdUsuario={0} ");
                //sql = string.Concat(sql, " END");
                //sql = string.Format(sql, idUsuario, idPerfil, idContratista);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "AsociarPerfil",
                    Codigo = this.Codigo + 4,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, idPerfil, idContratista }),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "AsociarPerfil",
                    Codigo = this.Codigo + 4,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { idUsuario, idPerfil, idContratista }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public int Modificar(Usuario usuario)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                //string sql = "UPDATE USUARIO SET Nombre='{0}', email='{1}', Apellido='{3}', FechaModificacion=GETDATE(), Estado={4} WHERE idUsuario={2}";
                //sql = string.Format(sql, usuario.Nombre, usuario.Email, usuario.IdUsuario, usuario.Apellido, ((usuario.Estado) ? 1 : 0));

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 5,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 5,
                    Estado = "ERROR",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int ModificarClave(Usuario usuario)
        {

            try
            {
                //string sql = "UPDATE USUARIO SET Clave=convert(varchar(100),hashbytes('SHA1', '{0}'),1), FechaModificacion=GETDATE() WHERE idUsuario={1}";
                //sql = string.Format(sql, usuario.Clave, usuario.IdUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();


                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ModificarClave",
                    Codigo = this.Codigo + 6,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ModificarClave",
                    Codigo = this.Codigo + 6,
                    Estado = "ERROR",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });

                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int RecuperarClave(int idUsuario, string claveNueva)
        {

            try
            {
                //string sql = "UPDATE USUARIO SET Clave=convert(varchar(100),hashbytes('SHA1', '{0}'),1), FechaModificacion=GETDATE(), CambiaClave=1, Intentos=0 WHERE IdUsuario={1} ";
                //sql = string.Format(sql, claveNueva, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "RecuperarClave",
                    Codigo = this.Codigo + 7,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "RecuperarClave",
                    Codigo = this.Codigo + 7,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }


        public int CambiarClave(int idUsuario, string ClaveNueva)
        {

            try
            {
                //string sql = "UPDATE USUARIO SET Clave=convert(varchar(100),hashbytes('SHA1', '{0}'),1), FechaModificacion=GETDATE(), CambiaClave=0, Intentos=0 WHERE IdUsuario={1} ";
                //sql = string.Format(sql, ClaveNueva, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "CambiarClave",
                    Codigo = this.Codigo + 8,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, ClaveNueva }),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "CambiarClave",
                    Codigo = this.Codigo + 8,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, ClaveNueva }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int ModificarPerfil(Usuario usuario)
        {

            try
            {
                //string sql = "UPDATE UsuarioPerfil SET Estado={2} WHERE idUsuario={0} AND idPerfil={1}";
                //sql = string.Format(sql, usuario.IdUsuario, usuario.IdPerfil, (usuario.Estado) ? "1" : "0");

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ModificarPerfil",
                    Codigo = this.Codigo + 9,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ModificarPerfil",
                    Codigo = this.Codigo + 9,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int CrearFront(Usuario usuario)
        {

            try
            {
                //string sql = "INSERT INTO USUARIO (Rut, Nombre, Apellido, Email, Clave, intentos, CambiaClave, Estado, Activo)";
                //sql = string.Concat(sql, " Values ('{0}', '{1}', '{2}', '{3}', convert(varchar(100),hashbytes('SHA1', '{4}'),1), 0, 0, {5},1)");
                //sql = string.Format(sql, usuario.Rut, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Clave, ((usuario.Estado) ? 1 : 0));

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1; //LeerByRut(usuario.Rut).IdUsuario;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int Crear(Usuario usuario)
        {

            try
            {
                //string sql = "INSERT INTO USUARIO (Rut, Nombre, Apellido, Email, Clave, intentos, CambiaClave, Estado, Activo)";
                //sql = string.Concat(sql, " Values ('{0}', '{1}', '{2}', '{3}', convert(varchar(100),hashbytes('SHA1', '{4}'),1), 0, 1, {5},1)");
                //sql = string.Format(sql, usuario.Rut, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Clave, ((usuario.Estado) ? 1 : 0));

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(usuario),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public (int Son, bool Activo) Reintentos(string email, string rut, string servicio)
        {
            (int Son, bool Activo) tuple;
            tuple  = (0, true);
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand("UsuarioReintentos", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                //cmd.Parameters.Add("@Rut", SqlDbType.VarChar, 10).Value = rut;
                //cmd.Parameters.Add("@Servicio", SqlDbType.VarChar, 10).Value = servicio;

                //SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    int son = 0;
                //    bool Activo = false;
                //    while (dr.Read())
                //    {
                //        son = int.Parse(dr["Son"].ToString());
                //        Activo = Boolean.Parse(dr["Activo"].ToString());
                //    }
                //    tuple = (son, Activo);
                //}
                //else
                //{
                //    tuple = (0, false);
                //}

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Reintentos",
                    Codigo = this.Codigo + 11,
                    Estado = "OK",
                    Entrada = js.Serialize(new { email, rut }),
                    Salida = js.Serialize(tuple)
                });

                return tuple;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Reintentos",
                    Codigo = this.Codigo + 11,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { email, rut }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return (-1, false);
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int ReiniciarClave(int idUsuario, string claveNueva)
        {

            try
            {
                //string sql = "UPDATE USUARIO SET Clave=convert(varchar(100),hashbytes('SHA1', '{0}'),1), FechaModificacion=GETDATE(), CambiaClave=1, Intentos=0, activo=1 WHERE idUsuario={1} ";
                //sql = string.Format(sql, claveNueva, idUsuario);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ReiniciarClave",
                    Codigo = this.Codigo + 12,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return 1;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "ReiniciarClave",
                    Codigo = this.Codigo + 12,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int GeneraSesion(int idUsuario, int idPerfil, string guid, string json)
        {

            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand("UsuarioLogin", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                //cmd.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = idPerfil;
                //cmd.Parameters.Add("@Guid", SqlDbType.VarChar, 100).Value = guid;
                //cmd.Parameters.Add("@Request", SqlDbType.VarChar, -1).Value = json;

                //SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                    //int son = 0;
                    //while (dr.Read())
                    //{
                    //    son = int.Parse(dr["Son"].ToString());
                    //}
                    return 1;
                //}
                //else
                //{
                //   return 0;
                //}

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Usuario Login(string email, string rut, string clave, string servicio)
        {


            try
            {
                //string sqlConsulta = string.Format("select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil, up.IdContratista from usuario u " +
                //                                " inner join UsuarioPerfil up on(u.IdUsuario = up.IdUsuario) " +
                //                                " inner join Perfil p on(p.IdPerfil = up.IdPerfil) " +
                //                                " WHERE (u.Email='{0}' or '{0}' = '') " +
                //                                " AND (u.Rut='{2}' or '{2}' = '') " +
                //                                " AND u.Clave=convert(varchar(100),hashbytes('SHA1', '{1}'),1) " +
                //                                " AND (p.ServicioPerfil='{3}') " +
                //                                " AND up.Estado=1 ", email, clave, rut, servicio);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sqlConsulta, con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                //}
                //else
                    return new Usuario();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public Usuario Login(int idUsuario, string claveProvisoria)
        {

            try
            {
                //string sqlConsulta = string.Format("select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil, up.IdContratista from usuario u " +
                //                                " inner join UsuarioPerfil up on(u.IdUsuario = up.IdUsuario) " +
                //                                " inner join Perfil p on(p.IdPerfil = up.IdPerfil) " +
                //                                " WHERE u.IdUsuario={0} " +
                //                                " AND u.Clave=convert(varchar(100),hashbytes('SHA1', '{1}'),1) " +
                //                                " AND up.Estado=1 ", idUsuario, claveProvisoria);

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(sqlConsulta, con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                //}
                //else
                    return new Usuario();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public List<Usuario> Listar(int perfil, string servicioPerfil = "BAK")
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(string.Format("select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil, up.IdContratista from usuario u " +
                //                                " inner join UsuarioPerfil up on(u.IdUsuario = up.IdUsuario) " +
                //                                " inner join Perfil p on(p.IdPerfil = up.IdPerfil) " +
                //                                " WHERE (up.IdPerfil={0} OR 0={0}) and (p.ServicioPerfil='{1}' OR ''='{1}') order by u.idUsuario desc ;", perfil, servicioPerfil), con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader);
                //}
                //else
                    return new List<Usuario>();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        /// <summary>
        /// Lista Usuarios del Sistema que pueden loguearse
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Usuario Leer(string email)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(string.Format("select u.* FROM Usuario u WHERE u.email='{0}' order by u.idUsuario desc ;", email), con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                //}
                //else
                    return new Usuario();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public Usuario LeerByRut(string rut)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(string.Format("select u.* FROM Usuario u WHERE u.Rut='{0}' order by u.idUsuario desc ;", rut), con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                //}
                //else
                    return new Usuario();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public Usuario Leer(int IdUsuario)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(string.Format("select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil, up.IdContratista from usuario u " +
                //                                " inner join UsuarioPerfil up on(u.IdUsuario = up.IdUsuario) " +
                //                                " inner join Perfil p on(p.IdPerfil = up.IdPerfil) " +
                //                                " WHERE (u.IdUsuario = {0}) order by u.idUsuario desc ;", IdUsuario), con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                //}
                //else
                    return new Usuario();


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }



    }
}
