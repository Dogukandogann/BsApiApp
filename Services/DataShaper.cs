using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDatas(entities, requiredProperties);
        }

        public ExpandoObject ShapeData(T entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entities, requiredProperties);
        }
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFileds = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields= fieldsString.Split(',',StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (property is null)
                        continue;
                    requiredFileds.Add(property);
                }
            }
            else
            {
                requiredFileds = Properties.ToList();
            }
            return requiredFileds;
        }
        private ExpandoObject FetchDataForEntity(T entity,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();

            foreach (var property in requiredProperties)
            {
                var objectPropertyvalue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name,objectPropertyvalue);
            }
            return shapedObject;
        }
        private IEnumerable<ExpandoObject> FetchDatas(IEnumerable<T> entities,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
