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
    public class ServicioProducto
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Producto";
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
        public ServicioProducto(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public Producto Crear(Producto Producto)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PRODUCTO.SP_Crear";
                cmd.Connection = con;

                //cmd.Parameters.Add(new OracleParameter("p_IdClienteExterno", OracleDbType.Int32, Producto.IdClienteExterno, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_IdClienteInterno", OracleDbType.Int32, Producto.IdClienteInterno, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_EstadoProducto", OracleDbType.Varchar2, Producto.Estado, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_PrecioVenta", OracleDbType.Double, Producto.PrecioVenta, System.Data.ParameterDirection.Input));

                //Salidas OUPUT
                cmd.Parameters.Add(new OracleParameter("p_IdProducto", OracleDbType.Int32, System.Data.ParameterDirection.Output));
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
                        Entrada = js.Serialize(Producto),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje= cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return Producto;
                }
                else
                {
                    Producto.IdProducto = int.Parse(cmd.Parameters["p_IdProducto"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(Producto),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return Producto;

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
                    Entrada = js.Serialize(Producto),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Producto Modificar(Producto Producto)
        {
            DataSet dataset = new DataSet();
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PRODUCTO.SP_Modificar";
                cmd.Connection = con;

                cmd.Parameters.Add(new OracleParameter("p_IdProducto", OracleDbType.Int32, Producto.IdProducto, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_EstadoProducto", OracleDbType.Varchar2, Producto.Estado, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_PrecioVenta", OracleDbType.Double, Producto.PrecioVenta, System.Data.ParameterDirection.Input));
                //cmd.Parameters.Add(new OracleParameter("p_FirmaContrato", OracleDbType.Char, Producto.FirmaContrato ? "1":"0" , System.Data.ParameterDirection.Input)); 
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
                        Entrada = js.Serialize(Producto),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return Producto;
                }                
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(Producto),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return Producto;

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
                    Entrada = js.Serialize(Producto),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        
        public int Eliminar(int IdProducto)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                string sql = "DELETE FROM Producto WHERE IdProducto={0}";
                sql = string.Format(sql, IdProducto);

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
                    Entrada = js.Serialize(IdProducto),
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
                    Entrada = js.Serialize(IdProducto),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
       
        public Producto Leer(int idProducto)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "";
                vSql = "select * from Producto  " +
                       "WHERE IdProducto={0} ";
                vSql = string.Format(vSql, idProducto);
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Producto>(reader).FirstOrDefault();
                listado.Categoria = new ServicioUtil().ListarCategorias().Where(x=>x.IdCategoria.Equals(listado.IdCategoria)).FirstOrDefault();                
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

        public List<Producto> Listar()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();

                string vSql = "";
                vSql = "select * from Producto  " +
                       " ORDER BY IdProducto DESC ";
                OracleCommand cmd = new OracleCommand(vSql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Producto>(reader);
                var categorias = new ServicioUtil().ListarCategorias();
                foreach (var item in listado)
                {
                    item.Categoria = categorias.Where(x => x.IdCategoria.Equals(item.IdCategoria)).FirstOrDefault();
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

        public List<Producto> Listar(int id)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PRODUCTO.SP_Listar";

                cmd.Parameters.Add(new OracleParameter("p_IdProductor", OracleDbType.Int32, id, System.Data.ParameterDirection.Input));
                OracleParameter oraP = new OracleParameter("p_glosa", OracleDbType.Varchar2, 2000);
                oraP.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP);
                cmd.Parameters.Add(new OracleParameter("p_estado", OracleDbType.Int32, System.Data.ParameterDirection.Output));

                OracleParameter oraP1 = new OracleParameter();
                oraP1.OracleDbType = OracleDbType.RefCursor;
                oraP1.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(oraP1);

                OracleDataReader reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Producto>(reader);
                var categorias = new ServicioUtil().ListarCategorias();
                foreach (var item in listado)
                {
                    item.Categoria = categorias.Where(x => x.IdCategoria.Equals(item.IdCategoria)).FirstOrDefault();
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


    }
}
