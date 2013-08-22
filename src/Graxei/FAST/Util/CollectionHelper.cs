using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace FAST.Utils
{
    /// <summary>
    /// Classe para funções com Collections
    /// </summary>
    public static class CollectionHelper
    {
        #region Public Static Methods
        /// <summary>
        /// Converte um List em um DataTable usando Reflection
        /// </summary>
        /// <typeparam name="T">Tipo da lista</typeparam>
        /// <param name="list">List de entrada</param>
        /// <returns>DataTable com as informações da lista</returns>
        public static DataTable ConvertList2DataTable<T>(ICollection<T> list)
        {
            Type type = typeof(T);
            PropertyInfo[] propertiesInfo = type.GetProperties();
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.CurrentCulture;
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            foreach (T item in list)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null);
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        #endregion
    }
}
