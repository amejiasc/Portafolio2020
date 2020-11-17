using FERIA.CLASES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FERIA.STORE
{
    public class ServicioSubasta
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Subasta";
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
        public ServicioSubasta(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public int Crear(Subasta subasta)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = "DECLARE p_id INTEGER; ";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "p_id := SQ_SUBASTA.nextval; ");
                sql = string.Concat(sql, "INSERT INTO Subasta (IDSUBASTA, FECHASUBASTA, FECHATERMINO, ");
                sql = string.Concat(sql, "IDPROCESO, ESTADO) ");
                sql = string.Concat(sql, "VALUES (p_id, '{0}', '{1}', {2}, '{3}'); ");                
                sql = string.Concat(sql, "END; ");
                sql = string.Format(sql, subasta.FechaSubasta.ToString("dd-MM-yyyy"), subasta.FechaTermino.ToString("dd-MM-yyyy"), subasta.IdProceso, subasta.Estado ? "1":"0" );

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
                    SubServicio = "Crear",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(subasta),
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
                    Entrada = js.Serialize(subasta),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public int Modificar(Subasta subasta)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = " ";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "UPDATE Subasta SET ");
                sql = string.Concat(sql, "FECHATERMINO='{1}', ");
                sql = string.Concat(sql, "ESTADO='{2}' ");
                sql = string.Concat(sql, "WHERE IdSubasta={0}; ");
                sql = string.Concat(sql, "END; ");
                sql = string.Format(sql, subasta.IdSubasta, subasta.FechaTermino.ToString("dd-MM-yyyy"), subasta.Estado ? "1" : "0");

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
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(subasta),
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
                    SubServicio = "Modificar",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(subasta),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public List<Subasta> Listar()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Subasta ORDER BY IdSubasta DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Subasta>(reader);
                foreach (var item in listado)
                {
                    item.DetalleSubasta = ListarDetalle(item.IdSubasta);
                }
                return listado;
            }
            catch (Exception ex)
            {
                return new List<Subasta>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Subasta> Listar(int idProceso)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Subasta WHERE IdProceso="+ idProceso + " ORDER BY IdSubasta DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                var listado = PopulateList.Filled<Subasta>(reader);
                foreach (var item in listado)
                {
                    item.DetalleSubasta  = ListarDetalle(item.IdSubasta );
                }
                return listado;
            }
            catch (Exception ex)
            {
                return new List<Subasta>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public Subasta Leer(int id)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Subasta WHERE IdSubasta=" + id, con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();
                var listado = PopulateList.Filled<Subasta>(reader).FirstOrDefault();
                if (listado != null)
                {
                    listado.DetalleSubasta = ListarDetalle(listado.IdSubasta);
                }
                return listado;
            }
            catch (Exception ex)
            {
                return new Subasta();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        #region "Detalle Subasta"
        public List<DetalleSubasta> ListarDetalle(int idSubasta)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from DetalleSubasta WHERE Idsubasta=" + idSubasta + " ORDER BY IdDetalle DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<DetalleSubasta>(reader);
            }
            catch (Exception ex)
            {
                return new List<DetalleSubasta>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        #endregion
    }
}
