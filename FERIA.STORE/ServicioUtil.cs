using FERIA.CLASES;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
