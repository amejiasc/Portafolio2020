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
    public class ServicioUtil
    {

        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "UTIL";
            }
        }
        private int Codigo
        {
            get
            {
                return 100;

            }
        }
        public string IdSession { get; set; }
        public ServicioUtil(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }

        public int CrearCategoria(Categoria categoria)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = "DECLARE p_id INTEGER; ";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "p_id := SQ_CATEGORIA.nextval; ");
                sql = string.Concat(sql, "INSERT INTO CATEGORIA (IDCATEGORIA, NOMBRECATEGORIA, NOMBREINGLES, ");
                sql = string.Concat(sql, "DESCRIPCION, DESCRIPCIONINGLES, ESTADO, FECHACREACION, FECHAMODIFICACION, IDUSUARIO) ");
                sql = string.Concat(sql, "VALUES (p_id, '{0}', '{1}', '{2}', '{3}', '{4}', sysdate, sysdate, ");
                sql = string.Concat(sql, "{5}); ");
                sql = string.Concat(sql, "END; ");
                sql = string.Format(sql, categoria.NombreCategoria, categoria.NombreIngles,
                                    categoria.Descripcion, categoria.DescripcionIngles, categoria.Estado ? "1": "0", categoria.IdUsuario);

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
                    SubServicio = "CrearCategoria",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(categoria),
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
                    SubServicio = "CrearCategoria",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(categoria),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public int ModificarCategoria(Categoria categoria)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = "";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "UPDATE CATEGORIA SET NOMBRECATEGORIA='{1}', NOMBREINGLES='{2}', ");
                sql = string.Concat(sql, "DESCRIPCION='{3}', DESCRIPCIONINGLES='{4}', ESTADO='{5}', FECHAMODIFICACION=sysdate ");
                sql = string.Concat(sql, "WHERE IdCategoria={0} ");
                sql = string.Concat(sql, "END; ");
                sql = string.Format(sql, categoria.IdCategoria, categoria.NombreCategoria, categoria.NombreIngles,
                                    categoria.Descripcion, categoria.DescripcionIngles, categoria.Estado ? "1" : "0", categoria.IdUsuario);

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
                    SubServicio = "ModificarCategoria",
                    Codigo = this.Codigo + 10,
                    Estado = "OK",
                    Entrada = js.Serialize(categoria),
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
                    SubServicio = "ModificarCategoria",
                    Codigo = this.Codigo + 10,
                    Estado = "ERROR",
                    Entrada = js.Serialize(categoria),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }

        public List<Region> ListarRegiones()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT DISTINCT IdRegion, NombreRegion from Comuna  ORDER BY IdRegion ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Region>(reader);

            }
            catch (Exception)
            {
                return new List<Region>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Comuna> ListarComunas()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Comuna ORDER BY NombreComuna ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Comuna>(reader);
                //return new List<Comuna>();

            }
            catch (Exception ex)
            {
                return new List<Comuna>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Categoria> ListarCategorias()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Categoria ORDER BY NombreCategoria ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Categoria>(reader);
                //return new List<Comuna>();

            }
            catch (Exception ex)
            {
                return new List<Categoria>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Perfil> ListarPerfiles()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Perfil ORDER BY NombrePerfil ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                //con.Open();
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Perfil>(reader);
                //return new List<Comuna>();

            }
            catch (Exception ex)
            {
                return new List<Perfil>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }



    }
}
