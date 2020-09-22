using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FERIA.NEGOCIO.Funciones
{
    public static class Varias
    {

        public static string TraduceDias(DayOfWeek dia)
        {
            string _Retorno = "No especificado";
            switch (dia)
            {
                case DayOfWeek.Sunday:
                    _Retorno = "Domingo";
                    break;
                case DayOfWeek.Monday:
                    _Retorno = "Lunes";
                    break;
                case DayOfWeek.Tuesday:
                    _Retorno = "Martes";
                    break;
                case DayOfWeek.Wednesday:
                    _Retorno = "Miércoles";
                    break;
                case DayOfWeek.Thursday:
                    _Retorno = "Jueves";
                    break;
                case DayOfWeek.Friday:
                    _Retorno = "Viernes";
                    break;
                case DayOfWeek.Saturday:
                    _Retorno = "Sábado";
                    break;
                default:
                    break;
            }
            return _Retorno;
        }

        public static bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        public static string FormatearRut(string rut)
        {
            try
            {
                if (!rut.Contains("-"))
                {
                    string dv = rut.Substring(rut.Length - 1);
                    rut = string.Concat(rut.Substring(0, rut.Length - 1), "-", dv);

                }
                string[] _rut = rut.ToString().Split('-');
                return string.Concat(double.Parse(_rut[0]).ToString("N0", CultureInfo.CreateSpecificCulture("es-CL")), "-", _rut[1]);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string FormatearMMAAAA(int FechaAAAAMM)
        {
            try
            {
                string _ret = "0";
                if (!FechaAAAAMM.Equals(0))
                {
                    _ret = FechaAAAAMM.ToString().Substring(4, 2) + "-" + FechaAAAAMM.ToString().Substring(0, 4);
                }

                return _ret;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string FormatearAAAAMM(int FechaAAAAMM)
        {
            try
            {
                string _ret = "0";
                if (!FechaAAAAMM.Equals(0))
                {
                    _ret = FechaAAAAMM.ToString().Substring(0, 4) + "-" + FechaAAAAMM.ToString().Substring(4, 2);
                }

                return _ret;
            }
            catch (Exception)
            {
                return "";
            }
        }


        public static bool ValidarEmail(string email)
        {
            email = email.ToLower();
            try
            {
                //return System.Text.RegularExpressions.Regex.IsMatch(to, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string RandomPassword()
        {

            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@_-";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 8;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }
        public static string GetHtmlPlantilla(PlantillaDisponible tipoPlantilla)
        {
            string plantilla = "";
            switch (tipoPlantilla)
            {
                case PlantillaDisponible.Recuperar:
                    plantilla = "Recuperar.Html";
                    break;
                case PlantillaDisponible.Reserva:
                    plantilla = "Reserva.Html";
                    break;
                case PlantillaDisponible.ReservaOnline:
                    plantilla = "ReservaOnline.Html";
                    break;
                default:
                    break;
            }
            try
            {
                string RutaTemplate = HttpContext.Current.Server.MapPath("~/Content/Template/" + plantilla);
                string readText = File.ReadAllText(RutaTemplate);
                return readText;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + HttpContext.Current.Server.MapPath("~/Content/Template/" + plantilla);
            }
        }
        public enum PlantillaDisponible
        {
            Recuperar,
            Reserva,
            ReservaOnline
        }
    }
}
