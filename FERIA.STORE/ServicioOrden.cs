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
    public class ServicioOrden
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Orden";
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
        public ServicioOrden(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public Orden Crear(Orden orden)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_ORDEN.SP_Crear";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_IdClienteExterno", OracleDbType.Int32, orden.IdClienteExterno, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_IdClienteInterno", OracleDbType.Int32, orden.IdClienteInterno, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_EstadoOrden", OracleDbType.Varchar2, orden.Estado, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_PrecioVenta", OracleDbType.Double, orden.PrecioVenta, System.Data.ParameterDirection.Input));

                //Salidas OUPUT
                cmd.Parameters.Add(new OracleParameter("p_IdOrden", OracleDbType.Int32, System.Data.ParameterDirection.Output));
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
                        Entrada = js.Serialize(orden),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje= cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return orden;
                }
                else
                {
                    orden.IdOrden = int.Parse(cmd.Parameters["p_IdOrden"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(orden),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return orden;

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
                    Entrada = js.Serialize(orden),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Orden Modificar(Orden orden)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_ORDEN.SP_Modificar";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_IdOrden", OracleDbType.Int32, orden.IdOrden, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_EstadoOrden", OracleDbType.Varchar2, orden.Estado, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_PrecioVenta", OracleDbType.Double, orden.PrecioVenta, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_FirmaContrato", OracleDbType.Char, orden.FirmaContrato ? "1":"0" , System.Data.ParameterDirection.Input)); 
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
                        Entrada = js.Serialize(orden),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return orden;
                }                
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(orden),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return orden;

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
                    Entrada = js.Serialize(orden),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int CrearDetalle(int idOrden, DetalleOrden detalleOrden)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_ORDEN.SP_CrearDetalle";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_IdOrden", OracleDbType.Int32, idOrden, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_Cantidad", OracleDbType.Int32, detalleOrden.Cantidad, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_IdCategoria", OracleDbType.Int32, detalleOrden.IdCategoria, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_Monto", OracleDbType.Double, detalleOrden.Monto, System.Data.ParameterDirection.Input));

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
                        SubServicio = "CrearDetalle",
                        Codigo = this.Codigo + 11,
                        Estado = "ERROR",
                        Entrada = js.Serialize(new { idOrden, detalleOrden }),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return 0;
                }                
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "CrearDetalle",
                    Codigo = this.Codigo + 11,
                    Estado = "OK",
                    Entrada = js.Serialize(new { idOrden, detalleOrden }),
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
                    SubServicio = "CrearDetalle",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { idOrden, detalleOrden }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int Firmar(int IdOrden)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                string sql = "UPDATE ORDEN SET FIRMACONTRATO='1', FECHAFIRMACONTRATO=sysdate, Estado='VIGENTE' IdOrden={0}";
                sql = string.Format(sql, IdOrden);

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
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 5,
                    Estado = "OK",
                    Entrada = js.Serialize(IdOrden),
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
                    Entrada = js.Serialize(IdOrden),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int Eliminar(int IdOrden)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                string sql = "DELETE FROM ORDEN WHERE IdOrden={0}";
                sql = string.Format(sql, IdOrden);

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
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 5,
                    Estado = "OK",
                    Entrada = js.Serialize(IdOrden),
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
                    Entrada = js.Serialize(IdOrden),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int EliminarDetalle(int IdOrden)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                string sql = "DELETE FROM DETALLEORDEN WHERE IdOrden={0}";
                sql = string.Format(sql, IdOrden);

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
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 5,
                    Estado = "OK",
                    Entrada = js.Serialize(IdOrden),
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
                    Entrada = js.Serialize(IdOrden),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public Orden Leer(int idOrden)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "";
                vSql = "select * from Orden  " +
                       "WHERE IdOrden={0} ";
                vSql = string.Format(vSql, idOrden);
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Orden>(reader).FirstOrDefault();
                listado.DetalleOrden = ListarDetalle(listado.IdOrden);                
                return listado;

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

        public List<Orden> Listar()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "";
                vSql = "select * from Orden  " +
                       " ORDER BY IdOrden DESC ";
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Orden>(reader);
                foreach (var item in listado)
                {
                    item.DetalleOrden = ListarDetalle(item.IdOrden);
                }
                return listado;

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
        private List<DetalleOrden> ListarDetalle(int idOrden)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "";
                vSql = "select do.*, nc.NOMBRECATEGORIA, nc.NOMBREIngles from DetalleOrden do  " +
                       "INNER JOIN Categoria nc on (do.idcategoria = nc.idcategoria) " + 
                       "WHERE IdOrden={0} ";
                vSql = string.Format(vSql, idOrden);
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<DetalleOrden>(reader);

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
