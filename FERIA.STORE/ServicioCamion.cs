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
    public class ServicioCamion
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Camion";
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
        public ServicioCamion(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public Camion Crear(Camion camion)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_CAMION.SP_Crear";
                cmd.Connection = con;

                string[] excepcion = new string[] { "IdCamion"};
                foreach (var item in PopulateList.ParametrosOracle(camion, excepcion))
                {
                    cmd.Parameters.Add(item);
                }


                //Salidas OUPUT
                cmd.Parameters.Add(new OracleParameter("p_IdCamion", OracleDbType.Int32, System.Data.ParameterDirection.Output));
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
                        Entrada = js.Serialize(camion),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return camion;
                }
                else
                {
                    camion.IdCamion= int.Parse(cmd.Parameters["p_IdCamion"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(camion),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return camion;

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
                    Entrada = js.Serialize(camion),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Camion Modificar(Camion camion)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_CAMION.SP_Modificar";
                cmd.Connection = con;

                string[] excepcion = new string[] { "IdUsuario" };
                foreach (var item in PopulateList.ParametrosOracle(camion, excepcion))
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
                        Entrada = js.Serialize(camion),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    camion.IdCamion = 0;
                    return camion;
                }

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(camion),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return camion;

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
                    Entrada = js.Serialize(camion),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public List<Camion> Listar()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Camion ORDER BY IdCamion DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Camion>(reader);
                
                return listado;
            }
            catch (Exception ex)
            {
                return new List<Camion>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Camion> Listar(int idTransportista)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Camion WHERE IdUsuario=" + idTransportista + " ORDER BY IdCamion DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Camion>(reader);
             
                return listado;
            }
            catch (Exception ex)
            {
                return new List<Camion>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public Camion Leer(int id)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Camion WHERE IdCamion=" + id, con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();
                var listado = PopulateList.Filled<Camion>(reader).FirstOrDefault();
                
                return listado;
            }
            catch (Exception ex)
            {
                return new Camion();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
    }
}
