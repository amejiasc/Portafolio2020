using FERIA.CLASES;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Data;

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
                string sql = "UPDATE USUARIO SET Clave='{0}', FechaModificacion=sysdate, CambiaClave=1, Intentos=0 WHERE IdUsuario={1} ";
                sql = string.Format(sql, claveNueva, idUsuario);
                DataSet dataset = new DataSet("Result");
                OracleConnection conn = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(sql, conn);
                //Fill the DataSet with data from 'Products' database table
                int rows = cmd.ExecuteNonQuery();
                dataset.Tables.Add(new DataTable("Table"));
                dataset.Tables[0].Columns.Add("Filas", typeof(int));
                dataset.Tables[0].Rows.Add(rows);
                //servicioLogTrace.Grabar(new Log()
                //{
                //    IdSession = this.IdSession,
                //    Servicio = this.Servicio,
                //    SubServicio = "RecuperarClave",
                //    Codigo = this.Codigo + 7,
                //    Estado = "OK",
                //    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                //    Salida = js.Serialize(new { Respuesta = "OK" })
                //});
                return 1;
            }
            catch (Exception ex)
            {
                //servicioLogTrace.Grabar(new Log()
                //{
                //    IdSession = this.IdSession,
                //    Servicio = this.Servicio,
                //    SubServicio = "RecuperarClave",
                //    Codigo = this.Codigo + 7,
                //    Estado = "OK",
                //    Entrada = js.Serialize(new { idUsuario, claveNueva }),
                //    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                //});
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
                string sql = "UPDATE USUARIO SET Clave='{0}', FechaModificacion=sysdate, CambiaClave='0', Intentos=0 WHERE IdUsuario={1} ";
                sql = string.Format(sql, ClaveNueva, idUsuario);

                DataSet dataset = new DataSet("Result");
                OracleConnection conn = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(sql, conn);
                //Fill the DataSet with data from 'Products' database table
                int rows = cmd.ExecuteNonQuery();
                dataset.Tables.Add(new DataTable("Table"));
                dataset.Tables[0].Columns.Add("Filas", typeof(int));
                dataset.Tables[0].Rows.Add(rows);

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
        public (int Son, bool Activo) Reintentos(string rut, int tipoPerfil)
        {
            (int Son, bool Activo) tuple;
            tuple  = (0, true);
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_USUARIO.SP_Reintentos";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_RutUsuario", OracleDbType.Varchar2, rut.ToString(), System.Data.ParameterDirection.Input));                
                cmd.Parameters.Add(new OracleParameter("p_IdPerfil", OracleDbType.Int32, tipoPerfil, System.Data.ParameterDirection.Input));

                cmd.Parameters.Add(new OracleParameter("p_Son", OracleDbType.Int32, System.Data.ParameterDirection.Output));
                cmd.Parameters.Add(new OracleParameter("p_Activo", OracleDbType.Char,1, System.Data.ParameterDirection.Output));
                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["p_estado"].Value.ToString().Equals("0"))
                {
                    bool Activo = cmd.Parameters["p_estado"].Value.ToString().Equals("1");
                    tuple = (1, Activo);
                    return tuple;
                }
                else
                {
                    if (cmd.Parameters["p_estado"].Value.ToString().Equals("1"))
                    {
                        return tuple;
                    }
                    return tuple;
                }

            }
            catch (Exception ex)
            {   
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
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_USUARIO.SP_GeneraSesion";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_IdUsuario", OracleDbType.Int32, idUsuario , System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_Fecha", OracleDbType.Varchar2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_Guid", OracleDbType.Varchar2, guid, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_Json", OracleDbType.Varchar2, json, System.Data.ParameterDirection.Input));

                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["p_estado"].Value.ToString().Equals("0"))
                {
                    return 1;

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Usuario Login(Login login)
        {
            
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_USUARIO.SP_LoginUsuario";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_RutUsuario", OracleDbType.Varchar2, login.Rut.ToString(), System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_ClaveUsuario", OracleDbType.Varchar2, login.Clave.ToString(), System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_IdPerfil", OracleDbType.Int32, login.TipoPerfil, System.Data.ParameterDirection.Input));
                
                cmd.Parameters.Add(new OracleParameter("p_IdUsuario", OracleDbType.Int32, System.Data.ParameterDirection.Output));
                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["p_estado"].Value.ToString().Equals("0"))
                {
                    int IdUsuario = int.Parse(cmd.Parameters["p_IdUsuario"].Value.ToString());                   
                    switch (login.TipoPerfil)
                    {
                        case (int)TipoPerfil.Administrador:
                            return LeerAdmin(IdUsuario);
                        case (int)TipoPerfil.Productor:
                            return LeerProductor(IdUsuario);
                        case (int)TipoPerfil.Cliente_Externo:
                            return LeerClienteExterno(IdUsuario);
                        case (int)TipoPerfil.Cliente_Interno:
                            return LeerClienteInterno(IdUsuario);
                        case (int)TipoPerfil.Transportista:
                            return LeerTransportista(IdUsuario);
                        case (int)TipoPerfil.Consultor:
                            return Leer(IdUsuario);
                        default:
                            return new Usuario();
                    }
                    return new Usuario();
                }
                else 
                {
                    if (cmd.Parameters["p_estado"].Value.ToString().Equals("1"))
                    {
                        return new Usuario();
                    }
                    return null;
                }
                
            }
            catch (Exception ex)
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
                OracleConnection con = objConexion.ObtenerConexion();
                string vSql = "";
                vSql = "select u.* from usuario u " +
                           " WHERE (u.idusuario={0} and u.clave='{1}')";

                vSql = string.Format(vSql, idUsuario, claveProvisoria );

                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Usuario>(reader).FirstOrDefault();


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

                string vSql = "";
                if (!perfil.Equals(0))
                {
                    vSql = "select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil from usuario u " +
                           " inner join Perfil p on(p.IdPerfil = u.IdPerfil) " +
                           " WHERE (p.IdPerfil={0} and p.ServicioPerfil='{1}') order by u.idUsuario desc";

                    vSql = string.Format(vSql, perfil, servicioPerfil);
                }
                else
                {
                    vSql = "select u.*, p.NombrePerfil, p.CodigoPerfil, p.IdPerfil from usuario u " +
                           " inner join Perfil p on(p.IdPerfil = u.IdPerfil) " +
                           " WHERE (p.ServicioPerfil='{0}') order by u.idUsuario desc";

                    vSql = string.Format(vSql, servicioPerfil);
                }
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Usuario>(reader);

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
                string vSql = "SELECT Usuario.* from Usuario WHERE IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Usuario>(reader).FirstOrDefault();
                
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
        private Administrador LeerAdmin(int IdUsuario)
        {
            try
            {
                string vSql = "SELECT Usuario.*, Administrador.IdComuna from Usuario INNER JOIN Administrador ON (Usuario.IdUsuario = Administrador.IdUsuario) WHERE Usuario.IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Administrador>(reader).FirstOrDefault();

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
        private Productor LeerProductor(int IdUsuario)
        {
            try
            {
                string vSql = "SELECT Usuario.*, Productor.FIRMACONTRATO, FECHATERMINO, NOMBREPRODUCTOR," +
                              " RUTPRODUCTOR, PERMITESALDOS, Productor.IdComuna from Usuario " + 
                              " INNER JOIN Productor ON (Usuario.IdUsuario = Productor.IdUsuario) " + 
                              " WHERE Usuario.IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Productor>(reader).FirstOrDefault();

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
        private ClienteExterno LeerClienteExterno(int IdUsuario)
        {
            try
            {
                string vSql = "SELECT Usuario.*, ClienteExterno.NombreEmpresa, Ciudad, Pais from Usuario " +
                              " INNER JOIN ClienteExterno ON (Usuario.IdUsuario = ClienteExterno.IdUsuario) " +
                              " WHERE Usuario.IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<ClienteExterno>(reader).FirstOrDefault();

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
        private ClienteInterno LeerClienteInterno(int IdUsuario)
        {
            try
            {
                string vSql = "SELECT Usuario.*, ClienteInterno.NombreCliente, RutCliente, IdComuna from Usuario " +
                              " INNER JOIN ClienteExterno ON (Usuario.IdUsuario = ClienteExterno.IdUsuario) " +
                              " WHERE Usuario.IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<ClienteInterno>(reader).FirstOrDefault();

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
        private Transportista LeerTransportista(int IdUsuario)
        {
            try
            {
                string vSql = "SELECT Usuario.*, Transportista.FIRMACONTRATO, FECHATERMINO, NOMBRETransportista," +
                              " RUTTransportista, Transportista.IdComuna from Usuario " +
                              " INNER JOIN Transportista ON (Usuario.IdUsuario = Transportista.IdUsuario) " +
                              " WHERE Usuario.IDUsuario={0}";
                vSql = string.Format(vSql, IdUsuario);
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Transportista>(reader).FirstOrDefault();

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
