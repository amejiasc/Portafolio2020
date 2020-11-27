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

                string[] excepcion = new string[] { "IdProceso", "FechaMaxSubasta" };
                foreach (var item in PopulateList.ParametrosOracle(proceso, excepcion))
                {
                    cmd.Parameters.Add(item);
                }


                //Salidas OUPUT
                cmd.Parameters.Add(new OracleParameter("p_IDPROCESO", OracleDbType.Int32, System.Data.ParameterDirection.Output));
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

                string[] excepcion = new string[] { "FechaMaxSubasta" };
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
                    proceso.IdProceso = 0;
                    return proceso;
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

                string vSql = "SELECT pr.*, pp.FechaMaxSubasta FROM Proceso pr ";
                vSql = string.Concat(vSql, "left JOIN (");
                vSql = string.Concat(vSql, "select o.idproceso, NVL(Min(FechaCaducidad), to_date('01-01-1900')) AS FechaMaxSubasta from Oferta o ");
                vSql = string.Concat(vSql, "inner join producto p on(o.idproducto = p.idproducto) ");
                vSql = string.Concat(vSql, "where o.Estado = 'GANADA' GROUP BY o.idproceso ");
                vSql = string.Concat(vSql, ") pp ON pr.idproceso = pp.idproceso ORDER BY pr.IdProceso DESC ");

                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var procesos =  PopulateList.Filled<Proceso>(reader);
                var listarOrdenes = new ServicioOrden().Listar();
                var listarOfertas = new ServicioOferta().Listar();
                foreach (var item in procesos)
                {
                    var ofertas = listarOfertas.Where(x => x.IdProceso.Equals(item.IdProceso)).ToList();
                    item.Orden = listarOrdenes.FirstOrDefault(x=>x.IdOrden.Equals(item.IdOrden));
                    item.Ofertas = (ofertas == null) ? new List<Oferta>() : ofertas;
                }
                return procesos;

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
        public List<Proceso> ListarByIdProductor(int idProductor)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "SELECT p.* from Proceso p  " +
                              " inner join(select idproceso from oferta where idusuario = "+ idProductor + " group by idproceso)  ds " +
                              " ON(p.idproceso = ds.idproceso)  ORDER BY p.idproceso DESC";

                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var procesos = PopulateList.Filled<Proceso>(reader);
                var listarOrdenes = new ServicioOrden().Listar();
                var listarOfertas = new ServicioOferta().Listar();
                foreach (var item in procesos)
                {
                    var ofertas = listarOfertas.Where(x => x.IdProceso.Equals(item.IdProceso)).ToList();
                    item.Orden = listarOrdenes.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden));
                    item.Ofertas = (ofertas == null) ? new List<Oferta>() : ofertas;
                }
                return procesos;

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

        public List<Proceso> ListarByIdOrden(int idOrden)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "SELECT p.* from Proceso p  " +
                              " WHERE p.IdOrden="+ idOrden + "  ORDER BY p.idproceso DESC";

                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var procesos = PopulateList.Filled<Proceso>(reader);
                var listarOrdenes = new ServicioOrden().Listar();
                var listarOfertas = new ServicioOferta().Listar();
                foreach (var item in procesos)
                {
                    var ofertas = listarOfertas.Where(x => x.IdProceso.Equals(item.IdProceso)).ToList();
                    item.Orden = listarOrdenes.FirstOrDefault(x => x.IdOrden.Equals(item.IdOrden));
                    item.Ofertas = (ofertas == null) ? new List<Oferta>() : ofertas;
                }
                return procesos;

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
        public Proceso Leer(int idProceso)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "SELECT pr.*, pp.FechaMaxSubasta FROM Proceso pr ";
                vSql = string.Concat(vSql,"left JOIN ( ");
                vSql = string.Concat(vSql, "select o.idproceso, NVL(Min(FechaCaducidad), to_date('01-01-1900')) AS FechaMaxSubasta from Oferta o ");
                vSql = string.Concat(vSql, "inner join producto p on(o.idproducto = p.idproducto) ");
                vSql = string.Concat(vSql, "where o.Estado = 'GANADA' GROUP BY o.idproceso ");
                vSql = string.Concat(vSql, ") pp ON pr.idproceso = pp.idproceso WHERE pr.IdProceso = " + idProceso.ToString());

                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var proceso = PopulateList.Filled<Proceso>(reader).FirstOrDefault();
                var listarOrdenes = new ServicioOrden().Listar();
                var listarOfertas = new ServicioOferta().Listar();
                var ofertas = listarOfertas.Where(x => x.IdProceso.Equals(proceso.IdProceso)).ToList();
                proceso.Orden = listarOrdenes.FirstOrDefault(x => x.IdOrden.Equals(proceso.IdOrden));
                proceso.Ofertas = (ofertas == null) ? new List<Oferta>() : ofertas;
                return proceso;

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

        public int ActualizaEstadoProceso(int idProceso, string estado)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = " ";
                sql = string.Concat(sql, "UPDATE Proceso SET  ");
                sql = string.Concat(sql, "EstadoProceso = '{1}' ");
                sql = string.Concat(sql, "WHERE IdProceso = {0}");
                sql = string.Format(sql, idProceso, estado);

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
                    SubServicio = "ActualizaEstadoProceso",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idProceso, estado }),
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
                    SubServicio = "ActualizaEstadoProceso",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { idProceso, estado }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }


    }
}
