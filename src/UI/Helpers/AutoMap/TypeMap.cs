using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using CodeCampServer.UI.Models.AutoMap.ReflectionExtensions;

namespace CodeCampServer.UI.Models.AutoMap
{
	public class TypeMap
	{
		private readonly IList<PropertyMap> _propertyMaps = new List<PropertyMap>();

		public TypeMap(Type modelType, Type dtoType)
		{
			DtoType = dtoType;
			ModelType = modelType;
		}

		public Type DtoType { get; set; }
		public Type ModelType { get; set; }

		public ReadOnlyCollection<PropertyMap> GetPropertyMaps()
		{
			return new ReadOnlyCollection<PropertyMap>(_propertyMaps);
		}

		public void AddPropertyMap(PropertyMap propertyMap)
		{
			_propertyMaps.Add(propertyMap);
		}

		public string[] GetUnmappedPropertyNames()
		{
			var autoMappedProperties = _propertyMaps.Select(pm => pm.DtoProperty.Name);

			return DtoType.GetPublicGetProperties()
							.Select(p => p.Name)
							.Except(autoMappedProperties)
							.ToArray();
		}

		public PropertyMap FindOrCreatePropertyMapFor(PropertyInfo dtoProperty)
		{
			var propertyMap = _propertyMaps.FirstOrDefault(pm => pm.DtoProperty.Name.Equals(dtoProperty.Name));
			if (propertyMap == null)
			{
				propertyMap = new PropertyMap(dtoProperty);
				AddPropertyMap(propertyMap);
			}

			return propertyMap;
		}
	}
}