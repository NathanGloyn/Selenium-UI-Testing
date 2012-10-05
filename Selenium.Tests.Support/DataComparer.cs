using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace Selenium.Tests.Support
{
    public class DataComparer
    {
        /// <summary>
        /// Compares an object to data extracted from the db
        /// </summary>
        /// <param name="toCompare">actual object to compare</param>
        /// <param name="expected">expected data to compare object against</param>
        /// <returns>List of any properties that don't match</returns>
        /// <remarks>The columns in the data row must be named the same as the properties on the object or the data will not be compared</remarks>
        public static List<string> CheckObjectMatchesData(object toCompare, DataRow expected)
        {
            var errors = new List<string>();

            foreach (PropertyInfo property in toCompare.GetType().GetProperties())
            {
                if (expected.Table.Columns.Contains(property.Name))
                {
                    if (expected[property.Name].ToString().Trim() != property.GetValue(toCompare, null).ToString().Trim())
                    {
                        errors.Add(string.Format("{0} doesn't match\r\nExpected:{1}\r\nActual:{2}", property.Name, expected[property.Name].ToString(), property.GetValue(toCompare, null).ToString()));
                    }
                }

            }

            return errors;
        }

    }
}
