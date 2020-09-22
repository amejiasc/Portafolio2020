using FERIA.CLASES;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            try
            {
                string sql = "INSERT INTO LogTrace (IdSession, Servicio, SubServicio, Codigo, Entrada, Salida, Fecha, Estado)";
                sql = string.Concat(sql, " Values (CASE WHEN '{0}'='' THEN NULL ELSE '{0}' END, '{1}', '{2}', {3}, '{4}', '{5}', GETDATE(), '{6}')");
                sql = string.Format(sql, log.IdSession, log.Servicio, log.SubServicio, log.Codigo, log.Entrada, log.Salida, log.Estado);

                OracleConnection con = objConexion.ObtenerConexion();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader;
                reader = cmd.ExecuteReader();
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
