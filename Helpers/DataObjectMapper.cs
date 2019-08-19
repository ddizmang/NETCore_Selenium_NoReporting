using System.Collections.Generic;
using System.Data;

namespace NETCore_Selenium.Helpers
{
    public class DataObjectMapper<T> where T : new()
    {
        //TODO: Marked for Removal
        public List<T> MapResultsToObject(IDataReader reader)
        {
            List<T> objects = new List<T>();

            while (reader.Read())
            {
                objects.Add(MapRow(reader));
            }

            return objects;
        }

        private T MapRow(IDataReader reader)
        {
            T item = new T();

            var typeProperties = typeof(T).GetProperties();

            foreach (var property in typeProperties)
            {
                int ordinal = reader.GetOrdinal(property.Name);

                if (!reader.IsDBNull(ordinal))
                {
                    property.SetValue(item, reader[ordinal].ToString(), null);
                }
            }

            return item;
        }
    }
}
