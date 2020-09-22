using FERIA.CLASES;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FERIA.STORE
{
    public class ServicioLogTrace
    {
        Conexion objConexion;
        public ServicioLogTrace()
        {
            objConexion = new Conexion();
        }

        public void Grabar(Log log)
        {
            //if (string.IsNullOrEmpty(log.IdSession))
            //    return;
            int rows = 0;
            DataSet dataset = new DataSet("Result");
            try
            {
                /*string sql = "INSERT INTO LogTrace (IdSession, Servicio, SubServicio, Codigo, Entrada, Salida, Fecha, Estado)";
                sql = string.Concat(sql, " Values (CASE WHEN '{0}'='' THEN NULL ELSE '{0}' END, '{1}', '{2}', {3}, '{4}', '{5}', GETDATE(), '{6}')");
                sql = string.Format(sql, log.IdSession, log.Servicio, log.SubServicio, log.Codigo, log.Entrada, log.Salida, log.Estado);*/

                string sql = "DECLARE p_id INTEGER; ";
                sql = string.Concat(sql, "BEGIN ");
                sql = string.Concat(sql, "p_id := SQ_LOGTRACE.nextval; ");
                sql = string.Concat(sql, "INSERT INTO LogTrace (LogId, IdSession, Servicio, SubServicio, Entrada, Salida, Estado) ");
                sql = string.Concat(sql, " VALUES (p_id, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'); ");
                sql = string.Concat(sql, "END; ");

                sql = string.Format(sql, log.IdSession, log.Servicio, log.SubServicio, log.Entrada, log.Salida, log.Estado); 

                OracleConnection conn = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(sql, conn);
                //Fill the DataSet with data from 'Products' database table
                rows = cmd.ExecuteNonQuery();
                dataset.Tables.Add(new DataTable("Table"));
                dataset.Tables[0].Columns.Add("Filas", typeof(int));
                dataset.Tables[0].Rows.Add(rows);
            }
            catch (Exception)
            {
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

    }
}
