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

        private string strConexion = ""; 
        public OracleConnection conexion; //= new OracleConnection();

        public Conexion()
        {
            try
            {
                strConexion = ConfigurationManager.ConnectionStrings["OracleDb"].ConnectionString;
            }
            catch (Exception e) { 
            
            }
            if (conexion == null) {
                conexion = new OracleConnection(strConexion);
            }
        }

        public OracleConnection ObtenerConexion()
        {
            //FERIA_VIRTUAL            
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new OracleConnection(strConexion);
                }

                if (conexion.State != System.Data.ConnectionState.Open){
                    conexion.Open();
                }
                return conexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DescargarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
                conexion.Dispose();
            }
            return true;
        }
    }
}
