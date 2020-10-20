using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FERIA.STORE
{
    public class PopulateList
    {
        public static List<T> Filled<T>(OracleDataReader reader)
        {
            var result = new List<T>();
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                var data = new object[reader.FieldCount];
                reader.GetValues(data);

                var instance = (T)Activator.CreateInstance(typeof(T));

                for (var i = 0; i < data.Length; ++i)
                {
                    if (data[i] == DBNull.Value)
                    {
                        data[i] = null;
                    }

                    var property = properties.SingleOrDefault(x => x.Name.Equals(columns[i], StringComparison.InvariantCultureIgnoreCase));

                    if (property != null)
                    {
                        if (property.PropertyType == typeof(DateTime) && data[i] == null)
                        {
                            data[i] = DateTime.MinValue;
                        }
                        if (property.PropertyType == typeof(int) && data[i] == null)
                        {
                            data[i] = 0;
                        }

                        if (property.PropertyType == typeof(Boolean))
                        {
                            property.SetValue(instance, Convert.ChangeType(data[i].ToString() == "1", property.PropertyType));
                        }
                        else
                        {
                            property.SetValue(instance, Convert.ChangeType(data[i], property.PropertyType));
                        }

                    }
                }
                result.Add(instance);
            }
            return result;

        }
        public static List<OracleParameter> ParametrosOracle<T>(T clase, string[] excepciones = null)
        {
            excepciones = excepciones ?? new string[0];
            List<OracleParameter> oracleParameterCollection = new List<OracleParameter>();
            foreach (var _property in clase.GetType().GetProperties())
            {
                if (!excepciones.Contains<string>(_property.Name))
                {
                    var prop = clase.GetType().GetProperty(_property.Name);
                    if (_property.PropertyType == typeof(string))
                    {
                        oracleParameterCollection.Add(new OracleParameter("p_" + _property, OracleDbType.Varchar2, prop.GetValue(clase, null).ToString(), ParameterDirection.Input));
                    }
                    if (_property.PropertyType == typeof(double))
                    {
                        oracleParameterCollection.Add(new OracleParameter("p_" + _property, OracleDbType.Double, prop.GetValue(clase, null), ParameterDirection.Input));
                    }
                    if (_property.PropertyType == typeof(int))
                    {
                        oracleParameterCollection.Add(new OracleParameter("p_" + _property, OracleDbType.Int32, prop.GetValue(clase, null), ParameterDirection.Input));
                    }
                    if (_property.PropertyType == typeof(Boolean))
                    {
                        var boleano = ((bool)prop.GetValue(clase, null)) ? "1" : "0";
                        oracleParameterCollection.Add(new OracleParameter("p_" + _property, OracleDbType.Char, boleano, ParameterDirection.Input));
                    }
                    if (_property.PropertyType == typeof(DateTime))
                    {
                        var fecha = (DateTime)prop.GetValue(clase, null);
                        oracleParameterCollection.Add(new OracleParameter("p_" + _property, OracleDbType.Date, fecha, ParameterDirection.Input));
                    }
                }
            }
            return oracleParameterCollection;
        }
    }
}
