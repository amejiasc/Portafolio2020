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
        public Producto Crear(Producto producto)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PRODUCTO.SP_Crear";
                cmd.Connection = con;

                string[] excepcion = new string[] { "IdProducto", "FechaCreacion", "FechaModificacion" };
                foreach (var item in PopulateList.ParametrosOracle(producto, excepcion))
                {
                    cmd.Parameters.Add(item);
                }


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
                        Entrada = js.Serialize(producto),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje= cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return producto;
                }
                else
                {
                    producto.IdProducto = int.Parse(cmd.Parameters["p_IdProducto"].Value.ToString());
                }
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(producto),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return producto;

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
                    Entrada = js.Serialize(producto),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public Producto Modificar(Producto producto)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = objConexion.ObtenerConexion(); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PKG_PRODUCTO.SP_Modificar";
                cmd.Connection = con;

                string[] excepcion = new string[] { "FechaCreacion", "FechaModificacion" };
                foreach (var item in PopulateList.ParametrosOracle(producto, excepcion))
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
                        Entrada = js.Serialize(producto),
                        Salida = js.Serialize(new { Estado = cmd.Parameters["p_estado"].Value.ToString(), Mensaje = cmd.Parameters["p_glosa"].Value.ToString() })
                    });
                    return producto;
                }                
                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(producto),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

                return producto;

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
                    Entrada = js.Serialize(producto),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return null;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int  ModificarStock(int IdProducto, int stock)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = " ";
                sql = string.Concat(sql, "UPDATE Producto SET  ");
                sql = string.Concat(sql, "STOCK = STOCK - {0} ");
                sql = string.Concat(sql, "WHERE IDPRODUCTO = {1}");
                sql = string.Format(sql, stock, IdProducto);

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
                    SubServicio = "ModificarStock",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(new { IdProducto, stock}),
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
                    SubServicio = "ModificarStock",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(new { IdProducto, stock }),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
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
