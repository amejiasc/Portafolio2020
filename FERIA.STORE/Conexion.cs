using System;
using System.Collections.Generic;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.STORE
{
    internal class Conexion
    {
        
        private string strConexion = ConfigurationManager.ConnectionStrings["OracleDb"].ConnectionString;
        public OracleConnection conexion = new OracleConnection();

        public OracleConnection ObtenerConexion()
        {
            //FERIA_VIRTUAL
            conexion = new OracleConnection(strConexion);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DescargarConexion()
        {
            conexion.Dispose();
            return true;
        }
    }
}
