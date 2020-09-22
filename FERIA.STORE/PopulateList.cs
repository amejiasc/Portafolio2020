using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        property.SetValue(instance, Convert.ChangeType(data[i], property.PropertyType));
                    }
                }
                result.Add(instance);
            }
            return result;

        }
    }
}
