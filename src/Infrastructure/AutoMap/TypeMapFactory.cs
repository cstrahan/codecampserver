using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeCampServer.Core.Common.ReflectionExtensions;

namespace CodeCampServer.Infrastructure.AutoMap
{
	internal class TypeMapFactory
	{
		private readonly Type _sourceType;
		private readonly Type _destinationType;

		public TypeMapFactory(Type sourceType, Type destinationType)
		{
			_sourceType = sourceType;
			_destinationType = destinationType;
		}

		public TypeMap CreateTypeMap()
		{
			var typeMap = new TypeMap(_sourceType, _destinationType);

			PropertyInfo[] destProperties = _destinationType.GetPublicGetProperties();

			foreach (PropertyInfo destProperty in destProperties)
			{
				var propertyMap = new PropertyMap(destProperty);

				if (MapDestinationPropertyToSource(propertyMap, _sourceType, destProperty.Name))
				{
					typeMap.AddPropertyMap(propertyMap);
				}
			}
			return typeMap;
		}

		private static bool MapDestinationPropertyToSource(PropertyMap propertyMap, Type sourceType, string nameToSearch)
		{
			PropertyInfo[] sourceProperties = sourceType.GetPublicGetProperties();
			MethodInfo[] sourceNoArgMethods = sourceType.GetPublicNoArgMethods();

			TypeMember typeMember = TypeMemberFinder.FindTypeMember(sourceProperties, sourceNoArgMethods, nameToSearch);

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

					TypeMember snippetTypeMember = TypeMemberFinder.FindTypeMember(sourceProperties, sourceNoArgMethods, snippet.First);

					if (snippetTypeMember == null)
					{
						continue;
					}

					propertyMap.ChainTypeMember(snippetTypeMember);

					foundMatch = MapDestinationPropertyToSource(propertyMap, snippetTypeMember.GetMemberType(), snippet.Second);

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