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
    public class ServicioOferta
    {
        Conexion objConexion;
        JavaScriptSerializer js = new JavaScriptSerializer();
        STORE.ServicioLogTrace servicioLogTrace;
        private string Servicio
        {
            get
            {
                return "Oferta";
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
        public ServicioOferta(string idSession = "")
        {
            objConexion = new Conexion();
            this.servicioLogTrace = new STORE.ServicioLogTrace();
            this.IdSession = idSession;
        }
        public int Crear(Oferta oferta)
        {
            DataSet dataset = new DataSet();
            try
            {
                string sql = "DECLARE p_id INTEGER; ";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "p_id := SQ_OFERTA.nextval; ");
                sql = string.Concat(sql, "INSERT INTO Oferta (IDOFERTA, CANTIDAD, VALORUNITARIO, ");
                sql = string.Concat(sql, "IDPROCESO, ESTADO, FECHAOFERTA, IDUSUARIO, IDPRODUCTO) ");
                sql = string.Concat(sql, "VALUES (p_id, {0}, {1}, {2}, '{3}', sysdate, ");
                sql = string.Concat(sql, "{4}, {5}); ");                
                sql = string.Concat(sql, "END; ");
                sql = string.Format(sql, oferta.Cantidad, oferta.ValorUnitario, oferta.IdProceso, oferta.Estado, oferta.IdUsuario, oferta.IdProducto);

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
                    Entrada = js.Serialize(oferta),
                    Salida = js.Serialize(new { Respuesta = "OK" })
                });

               var retorno = new ServicioProducto(IdSession).ModificarStock(oferta.IdProducto, oferta.Cantidad);
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
                    Entrada = js.Serialize(oferta),
                    Salida = js.Serialize(new { ex.Message, ex.StackTrace, ex.Source, ex.InnerException })
                });
                return 0;
            }
            finally
            {
                objConexion.DescargarConexion();
            }

        }
        public List<Oferta> Listar()
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Oferta ORDER BY IdOferta DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Oferta>(reader);
            }
            catch (Exception ex)
            {
                return new List<Oferta>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public List<Oferta> Listar(int idProductor)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Oferta WHERE idProductor="+ idProductor + " ORDER BY IdOferta DESC", con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                return PopulateList.Filled<Oferta>(reader);
            }
            catch (Exception ex)
            {
                return new List<Oferta>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public Oferta Leer(int id)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Oferta WHERE IdOferta=" + id, con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();
                return PopulateList.Filled<Oferta>(reader).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Oferta();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
        public Oferta Leer(int idProducto, int idProceso)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand("SELECT * from Oferta WHERE IdProducto=" + idProducto + " and IdProceso=" + idProceso, con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();
                return PopulateList.Filled<Oferta>(reader).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Oferta();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }
    }
}
