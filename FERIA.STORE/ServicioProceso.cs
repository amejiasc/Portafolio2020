using FERIA.CLASES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FERIA.STORE
{
    public class ServicioProceso
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Proceso";
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
        public ServicioProceso(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public Proceso Crear(Proceso proceso)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PROCESO.SP_Crear";
                cmd.Connection = con;

                string[] excepcion = new string[] { "IdProceso" };
                foreach (var item in PopulateList.ParametrosOracle(proceso, excepcion))
                {
                    cmd.Parameters.Add(item);
                }


                //Salidas OUPUT
                cmd.Parameters.Add(new OracleParameter("IdProceso", OracleDbType.Int32, System.Data.ParameterDirection.Output));
                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                cmd.ExecuteNonQuery();

                if (!cmd.Parameters["p_estado"].Value.ToString().Equals("0"))
                {
                    servicioLogTrace.Grabar(new Log()
                    {
                        IdSession = this.IdSession,
                        Servicio = this.Servicio,
                        SubServicio = "Crear",
                        Codigo = this.Codigo + 10,
                        Estado = "ERROR",
                        Entrada = js.Serialize(proceso),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return proceso;
                }
                else
                {
                    proceso.IdProceso = int.Parse(cmd.Parameters["p_IdProceso"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(proceso),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return proceso;

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
                    Entrada = js.Serialize(proceso),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Proceso Modificar(Proceso proceso)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PROCESO.SP_Modificar";
                cmd.Connection = con;

                string[] excepcion = new string[] { };
                foreach (var item in PopulateList.ParametrosOracle(proceso, excepcion))
                {
                    cmd.Parameters.Add(item);
                }


                //Salidas OUPUT
                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                cmd.ExecuteNonQuery();

                if (!cmd.Parameters["p_estado"].Value.ToString().Equals("0"))
                {
                    servicioLogTrace.Grabar(new Log()
                    {
                        IdSession = this.IdSession,
                        Servicio = this.Servicio,
                        SubServicio = "Modificar",
                        Codigo = this.Codigo + 10,
                        Estado = "ERROR",
                        Entrada = js.Serialize(proceso),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return proceso;
                }
                else
                {
                    proceso.IdProceso = int.Parse(cmd.Parameters["p_IdProceso"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(proceso),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return proceso;

            }
            catch (Exception ex)
            {
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(proceso),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public List<Proceso> Listar()
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "SELECT * FROM PROCESO";
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Proceso>(reader);

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
