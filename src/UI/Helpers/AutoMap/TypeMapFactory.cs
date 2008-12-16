using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeCampServer.UI.Models.AutoMap.ReflectionExtensions;

namespace CodeCampServer.UI.Models.AutoMap
{
	internal class TypeMapFactory
	{
		private readonly Type _modelType;
		private readonly Type _dtoType;

		public TypeMapFactory(Type modelType, Type dtoType)
		{
			_modelType = modelType;
			_dtoType = dtoType;
		}

		public TypeMap CreateTypeMap()
		{
			var typeMap = new TypeMap(_modelType, _dtoType);

			PropertyInfo[] dtoProperties = _dtoType.GetPublicGetProperties();

			foreach (PropertyInfo dtoProperty in dtoProperties)
			{
				var propertyMap = new PropertyMap(dtoProperty);

				if (MapDtoPropertyToModel(propertyMap, _modelType, dtoProperty.Name))
				{
					typeMap.AddPropertyMap(propertyMap);
				}
			}
			return typeMap;
		}

		private static bool MapDtoPropertyToModel(PropertyMap propertyMap, Type modelType, string nameToSearch)
		{
			PropertyInfo[] modelProperties = modelType.GetPublicGetProperties();
			MethodInfo[] modelNoArgMethods = modelType.GetPublicNoArgMethods();

			TypeMember typeMember = ReflectionHelper.FindTypeMember(modelProperties, modelNoArgMethods, nameToSearch);

			bool foundMatch = typeMember != null;

			if (!foundMatch)
			{
				string[] matches = Regex.Matches(nameToSearch, "[A-Z][a-z0-9]*")
					.Cast<Match>()
					.Select(m => m.Value)
					.ToArray();

				for (int i = 0; i < matches.Length - 1; i++)
				{
					NameSnippet snippet = CreateNameSnippet(matches, i);

					TypeMember snippetTypeMember = ReflectionHelper.FindTypeMember(modelProperties, modelNoArgMethods, snippet.First);

					if (snippetTypeMember == null)
					{
						continue;
					}

					propertyMap.ChainTypeMember(snippetTypeMember);

					foundMatch = MapDtoPropertyToModel(propertyMap, snippetTypeMember.GetMemberType(), snippet.Second);

					if (foundMatch)
					{
						break;
					}

					propertyMap.RemoveLastModelProperty();
				}
			}
			else
			{
				propertyMap.ChainTypeMember(typeMember);
			}

			return foundMatch;
		}


		private static NameSnippet CreateNameSnippet(string[] matches, int i)
		{
			return new NameSnippet
			       	{
			       		First = String.Concat(matches.Take(i + 1).ToArray()),
			       		Second = String.Concat(matches.Skip(i + 1).ToArray())
			       	};
		}

		private class NameSnippet
		{
			public string First { get; set; }
			public string Second { get; set; }
		}
	}
}