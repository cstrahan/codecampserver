using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeCampServer.Core;

namespace CodeCampServer.UI.Models.AutoMap
{
	public static class AutoMapper
	{
		private static readonly IList<TypeMap> _typeMaps = new List<TypeMap>();
		private static ValueFormatter _formatters = new ValueFormatter();

		public static TDto Map<TModel, TDto>(TModel input)
		{
			Type modelType = typeof (TModel);
			Type dtoType = typeof (TDto);

			return (TDto) Map(input, modelType, dtoType);
		}
		
		public static object Map(object input, Type modelType, Type dtoType)
		{
			return Map(input, modelType, dtoType, new ResolutionContext(null, null, null, null, Equals(null, input)));
		}

		public static object Map(IEnumerable inputArray, Type modelArrayType, Type dtoArrayType, Type modelDerivedType, Type dtoDerivedType)
		{
			return CreatePolymorphicArrayObject(inputArray, dtoArrayType, modelDerivedType, dtoDerivedType, new ResolutionContext(null, null, null, null, Equals(null, inputArray)));
		}

		public static IMappingExpression<TModel, TDto> Configure<TModel, TDto>()
		{
			Type modelType = typeof (TModel);
			Type dtoType = typeof (TDto);

			var typeMapFactory = new TypeMapFactory(modelType, dtoType);
			TypeMap typeMap = typeMapFactory.CreateTypeMap();

			_typeMaps.Add(typeMap);

			return new MappingExpression<TModel, TDto>(typeMap);
		}

		public static TypeMap FindTypeMapFor(Type modelType, Type dtoType)
		{
			var typeMap = _typeMaps.FirstOrDefault(x => x.DtoType == dtoType && x.ModelType == modelType);
			
			if ((typeMap == null) && modelType.BaseType != null)
				return FindTypeMapFor(modelType.BaseType, dtoType);

			return typeMap;
		}

		public static TypeMap FindTypeMapFor<TModel, TDto>()
		{
			return FindTypeMapFor(typeof (TModel), typeof (TDto));
		}

		public static TypeMap[] GetAllTypeMaps()
		{
			return _typeMaps.ToArray();
		}

		public static void InitializeFormatting(Action<ICustomFormatterExpression> customFormatExpression)
		{
			var valueFormatter = new ValueFormatter();

			customFormatExpression(valueFormatter);

			_formatters = valueFormatter;
		}

		public static void Reset()
		{
			_formatters = new ValueFormatter();
			_typeMaps.Clear();
		}
		
		private static object Map(object input, Type modelType, Type dtoType, ResolutionContext context)
		{
			object valueToAssign;

			TypeMap typeMap = FindTypeMapFor(modelType, dtoType);

			if (typeMap != null)
			{
				valueToAssign = CreateMappedObject(input, dtoType, new ResolutionContext(typeMap, context.MemberName, context.PropertyMap, context.ModelMemberType, context.IsModelMemberValueNull));
			}
			else if (dtoType == typeof(bool) && modelType ==  typeof(bool?))
			{
				valueToAssign = input ?? false;
			}
			else if (input == null)
			{
				valueToAssign = CreateNullOrDefaultObject(dtoType, context);
			}
			else if (dtoType.Equals(typeof(string)))
			{
				valueToAssign = FormatDataElement(input, context);
			}
			else if (dtoType.IsAssignableFrom(modelType))
			{
				valueToAssign = input;
			}
			else if ((dtoType.IsArray) && (input is IEnumerable))
			{
				valueToAssign = CreateArrayObject(input, dtoType, context);
			}
			else
			{
				valueToAssign = CreateNullOrDefaultObject(dtoType, context);
			}

			return valueToAssign;
		}

		private static object CreateMappedObject(object input, Type dtoType, ResolutionContext context)
		{
			object dto = CreateObject(dtoType);

			foreach (PropertyMap propertyMap in context.TypeMap.GetPropertyMaps())
			{
				object modelMemberValue;
				Type modelMemberType;

				IValueResolver resolver = propertyMap.GetCustomValueResolver();

				if (resolver != null)
				{
					var inputValueToResolve = input;

					if (propertyMap.HasMembersToResolveForCustomResolver)
					{
						inputValueToResolve = ResolveModelMemberValue(propertyMap, input);
					}

					modelMemberValue = resolver.Resolve(inputValueToResolve);
					modelMemberType = modelMemberValue != null ? modelMemberValue.GetType() : typeof (object);
				}
				else
				{
					modelMemberValue = ResolveModelMemberValue(propertyMap, input);

					TypeMember modelMemberToUse = propertyMap.GetLastModelMemberInChain();
					modelMemberType = modelMemberToUse.GetMemberType();
				}

				PropertyInfo dtoProperty = propertyMap.DtoProperty;
				Type dtoPropertyType = dtoProperty.PropertyType;

				object propertyValueToAssign = Map(modelMemberValue, modelMemberType, dtoPropertyType, new ResolutionContext(context.TypeMap, dtoProperty.Name, propertyMap, modelMemberType, modelMemberValue == null));

				dtoProperty.SetValue(dto, propertyValueToAssign, new object[0]);
			}

			return dto;
		}

		private static object ResolveModelMemberValue(PropertyMap propertyMap, object input)
		{
			object modelMemberValue = input;

			if (modelMemberValue != null)
			{
				foreach (TypeMember modelProperty in propertyMap.GetModelMemberChain())
				{
					modelMemberValue = modelProperty.GetValue(modelMemberValue);

					if (modelMemberValue == null)
					{
						break;
					}
				}
			}
			return modelMemberValue;
		}

		private static object CreateArrayObject(object input, Type dtoType, ResolutionContext context)
		{
			IEnumerable<object> enumerableValue = ((IEnumerable) input).Cast<object>();

			Type modelElementType = input.GetType().GetElementType();
			Type dtoElementType = dtoType.GetElementType();

			Array dtoArrayValue = Array.CreateInstance(dtoElementType, enumerableValue.Count());

			enumerableValue.ForEach(
				(item, i) => dtoArrayValue.SetValue(Map(item, modelElementType, dtoElementType, new ResolutionContext(context.TypeMap, context.MemberName + i, context.PropertyMap, modelElementType, context.IsModelMemberValueNull)), i)
				);

			object valueToAssign = dtoArrayValue;
			return valueToAssign;
		}

		private static object CreatePolymorphicArrayObject(object input, Type dtoArrayType, Type modelDerivedType, Type dtoDerivedType, ResolutionContext context)
		{
			IEnumerable<object> enumerableValues = ((IEnumerable) input).Cast<object>();

			Type modelElementType = input.GetType().GetElementType();
			Type dtoElementType = dtoArrayType.GetElementType();

			Array dtoArrayValue = Array.CreateInstance(dtoElementType, enumerableValues.Count());

			for (int i = 0; i < enumerableValues.Count(); i++)
			{
				var inputValue = enumerableValues.ElementAt(i);

				if (inputValue.GetType() == modelDerivedType)
				{
					var mappedValue = Map(inputValue, modelDerivedType, dtoDerivedType, new ResolutionContext(FindTypeMapFor(modelDerivedType, dtoDerivedType), context.MemberName + i, context.PropertyMap, modelDerivedType, context.IsModelMemberValueNull));
					dtoArrayValue.SetValue(mappedValue, i);
				}
				else
				{
					dtoArrayValue.SetValue(Map(inputValue, modelElementType, dtoElementType, new ResolutionContext(context.TypeMap, context.MemberName + i, context.PropertyMap, modelElementType, context.IsModelMemberValueNull)), i); 
				}
			}

			object valueToAssign = dtoArrayValue;
			return valueToAssign;
		}

		private static object CreateNullOrDefaultObject(Type type, ResolutionContext context)
		{
			object valueToAssign;
			object nullValueToUse = null;

			if (context.PropertyMap != null)
				nullValueToUse = context.PropertyMap.GetNullValueToUse();

			if (type == typeof (string))
			{
				valueToAssign = FormatDataElement(nullValueToUse, context);
			}
			else if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				Array arrayValue = Array.CreateInstance(elementType, 0);
				valueToAssign = arrayValue;
			}
			else if (nullValueToUse != null)
			{
				valueToAssign = nullValueToUse;
			}
			else
			{
				valueToAssign = Activator.CreateInstance(type, true);
			}

			return valueToAssign;
		}

		private static object CreateObject(Type type)
		{
			return Activator.CreateInstance(type, true);
		}

		private static string FormatDataElement(object input, ResolutionContext context)
		{
			return ((IValueFormatter) _formatters).FormatValue(input, context);
		}
	}
}