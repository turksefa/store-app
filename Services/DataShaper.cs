using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class DataShaper<T> : IDataShaper<T>
		where T : class
	{
		public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
		{
			var shapedObjects = new List<ExpandoObject>();			

			foreach (var entity in entities)
			{
				shapedObjects.Add(ShapeData(entity, fieldsString));
			}
			return shapedObjects;
		}

		public ExpandoObject ShapeData(T entity, string fieldsString)
		{
			var propertyInfos = new List<PropertyInfo>();
			var properties = typeof(T).GetProperties();
			var fields = fieldsString.Split(",");
			foreach(var field in fields)
			{
				var property = properties.FirstOrDefault(pi => pi.Name.Equals(field));
				if (property == null)
					continue;
				propertyInfos.Add(property);
			}

			var shapedObject = new ExpandoObject();
			foreach (PropertyInfo property in propertyInfos)
			{
				shapedObject.TryAdd(property.Name, property.GetValue(entity));
			}
			return shapedObject;
		}
	}
}
