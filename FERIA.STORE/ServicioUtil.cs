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

        public int Crear(Codigo Codigo)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                //string vSql = "INSERT INTO Codigo ( " +
                //              "Tipo, " +
                //              "Valor, " +
                //              "Glosa, " +
                //              "CodigoTxt, " +
                //              "Estado " +
                //              ") " +
                //              "VALUES ( " +
                //              "'{0}', " +
                //              "{1}, " +
                //              "'{2}', " +
                //              "'{3}', " +
                //              "{4} " +
                //              ")";

                //vSql = string.Format(vSql,
                //                     Codigo.Tipo,
                //                     Codigo.Valor,
                //                     Codigo.Glosa,
                //                     Codigo.CodigoTxt,
                //                     Codigo.Estado ? 1 : 0
                //                     );

                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand(vSql, con);

                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                servicioLogTrace.Grabar(new Log()
                {
                    IdSession = this.IdSession,
                    Servicio = this.Servicio,
                    SubServicio = m.Name,
                    Codigo = this.Codigo + 1,
                    Estado = "OK",
                    Entrada = js.Serialize(Codigo),
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
                    Codigo = this.Codigo + 1,
                    Estado = "ERROR",
                    Entrada = js.Serialize(Codigo),
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
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT IdRegion, NombreRegion from Comuna  ORDER BY IdRegion ASC;", con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //return PopulateList.Filled<Region>(reader);
                return new List<Region>();

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
                //SqlCommand cmd = new SqlCommand("SELECT * from Comuna ORDER BY NombreComuna ASC;", con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //return PopulateList.Filled<Comuna>(reader);
                return new List<Comuna>();

            }
            catch (Exception)
            {
                return new List<Comuna>();
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
                //SqlCommand cmd = new SqlCommand("select * from perfil ORDER BY NombrePerfil ASC;", con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //return PopulateList.Filled<Perfil>(reader);
                return new List<Perfil>();
            }
            catch (Exception)
            {
                return new List<Perfil>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

        public List<Codigo> ListarEstados(string estado)
        {
            try
            {
                OracleConnection con = objConexion.ObtenerConexion();
                //SqlCommand cmd = new SqlCommand("select * from Codigo WHERE Tipo='" + estado + "' ORDER BY Glosa ASC;", con);
                //cmd.CommandType = CommandType.Text;
                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //return PopulateList.Filled<Codigo>(reader);
                return new List<Codigo>();
            }
            catch (Exception)
            {
                return new List<Codigo>();
            }
            finally
            {
                objConexion.DescargarConexion();
            }
        }

    }
}
