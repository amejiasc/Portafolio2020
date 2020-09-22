using FERIA.CLASES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioUtil
    {
        STORE.ServicioUtil servicioUtil;
        public ServicioUtil(string idSession = "")
        {
            this.servicioUtil = new STORE.ServicioUtil(idSession);
        }

        public bool Crear(Codigo codigo)
        {
            if (servicioUtil.ListarEstados(codigo.Tipo).Exists(x => x.Valor.Equals(codigo.Valor)))
            {
                return false;
            }
            if (servicioUtil.ListarEstados(codigo.Tipo).Exists(x => x.Glosa.ToLower().Contains(codigo.Glosa.ToLower())))
            {
                return false;
            }
            var id = servicioUtil.Crear(codigo);
            if (id == 0)
            {
                return false;
            }
            return true;
        }


        public List<Region> Regiones()
        {
            return servicioUtil.ListarRegiones();
        }
        public List<Comuna> Comunas()
        {
            return servicioUtil.ListarComunas();
        }
        public List<Perfil> ListarPerfiles()
        {
            return servicioUtil.ListarPerfiles();
        }
        public List<Codigo> ListarBusesGenero()
        {
            return servicioUtil.ListarEstados("TIPOBUS");
        }
        public List<Codigo> ListarMarcas()
        {
            return servicioUtil.ListarEstados("MARCA");
        }
        public List<Codigo> ListarEstadoServicio()
        {
            return servicioUtil.ListarEstados("ESTSERVICIO");
        }
        public List<Codigo> ListarTipoItinerario()
        {
            return servicioUtil.ListarEstados("TIPOITINERARIO");
        }
        public List<Codigo> ListarEstadoReserva()
        {
            return servicioUtil.ListarEstados("RESERVAESTADO");
        }
        public List<Codigo> ListarTurnos()
        {
            return servicioUtil.ListarEstados("TURNO");
        }
        //EMPRESA
        public List<Codigo> ListarEmpresaContratos()
        {
            return servicioUtil.ListarEstados("EMPRESA");
        }
        public List<Codigo> ListarUnidades()
        {
            return servicioUtil.ListarEstados("UNIDAD");
        }
        public List<Codigo> ListarPuestos()
        {
            return servicioUtil.ListarEstados("PUESTO");
        }
        public List<Codigo> ListarEstadoCivil()
        {
            return servicioUtil.ListarEstados("ESTADOCIVIL");
        }
        public List<Codigo> ListarEstadoCarga()
        {
            return servicioUtil.ListarEstados("ESTADOCARGA");
        }
        public List<Codigo> ListarSexo()
        {
            return servicioUtil.ListarEstados("SEXO");
        }
    }
}
