using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using CodeCampServer.Core;

namespace CodeCampServer.UI.Models.AutoMap
{
	internal class MappingExpression<TModel, TDto> : IMappingExpression<TModel, TDto>, IFormattingExpression<TModel>
	{
		private readonly TypeMap _typeMap;
		private PropertyMap _propertyMap;

		public MappingExpression(TypeMap typeMap)
		{
			_typeMap = typeMap;
		}

		public IMappingExpression<TModel, TDto> ForDtoMember(Expression<Func<TDto, object>> dtoMemberExpression,
		                                                     Action<IFormattingExpression<TModel>> memberOptions)
		{
			PropertyInfo dtoProperty = ReflectionHelper.FindDtoProperty(dtoMemberExpression);
			ForDtoMember(dtoProperty, memberOptions);
			return new MappingExpression<TModel, TDto>(_typeMap);
		}

		private void ForDtoMember(PropertyInfo dtoProperty,
		                          Action<IFormattingExpression<TModel>> memberOptions)
		{
			_propertyMap = _typeMap.FindOrCreatePropertyMapFor(dtoProperty);

			memberOptions(this);
		}

		public void ForAllMembers(Action<IFormattingExpression<TModel>> memberOptions)
		{
			_typeMap.GetPropertyMaps().ForEach(x => ForDtoMember(x.DtoProperty, memberOptions));
		}

		public void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
		{
			_propertyMap.AddFormatterToSkip<TValueFormatter>();
		}

		public void AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
		{
			var formatter = (IValueFormatter)Activator.CreateInstance(typeof(TValueFormatter), true);

			AddFormatter(formatter);
		}

		public void AddFormatter(IValueFormatter formatter)
		{
			_propertyMap.AddFormatter(formatter);
		}

		public void FormatNullValueAs(string nullSubstitute)
		{
			_propertyMap.FormatNullValueAs(nullSubstitute);
		}

		public IResolutionExpression<TModel> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver
		{
			var resolver = (IValueResolver)Activator.CreateInstance(typeof(TValueResolver), true);

			return ResolveUsing(resolver);
		}

		public IResolutionExpression<TModel> ResolveUsing(IValueResolver valueResolver)
		{
			_propertyMap.AssignCustomValueResolver(valueResolver);

			return new ResolutionExpression<TModel>(_propertyMap);
		}

		public void MapFromMember(Expression<Func<TModel, object>> modelMemberExpression)
		{
			var modelTypeMembers = BuildModelTypeMembers(modelMemberExpression);

			_propertyMap.ChainTypeMembers(modelTypeMembers);
		}

		private static TypeMember[] BuildModelTypeMembers(LambdaExpression lambdaExpression)
		{
			Expression expressionToCheck = lambdaExpression;
			var typeMembers = new List<TypeMember>();

			bool done = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
						break;
					case ExpressionType.Lambda:
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = ((MemberExpression)expressionToCheck);
						var propertyInfo = memberExpression.Member as PropertyInfo;
						if (propertyInfo != null)
						{
							typeMembers.Add(new PropertyMember(propertyInfo));
						}
						expressionToCheck = memberExpression.Expression;
						break;
					case ExpressionType.Call:
						var methodCallExpression = ((MethodCallExpression)expressionToCheck);
						typeMembers.Add(new MethodMember(methodCallExpression.Method));
						expressionToCheck = methodCallExpression.Object;
						break;
					default:
						done = true;
						break;
				}
				if (expressionToCheck == null)
					done = true;
			}

			// LINQ lists members in reverse
			typeMembers.Reverse();

			return typeMembers.ToArray();
		}

		private class ResolutionExpression<TResolutionModel> : IResolutionExpression<TResolutionModel>
		{
			private readonly PropertyMap _propertyMap;

			public ResolutionExpression(PropertyMap propertyMap)
			{
				_propertyMap = propertyMap;
			}

			public void FromModelMember(Expression<Func<TResolutionModel, object>> modelMemberExpression)
			{
				_propertyMap.ChainTypeMembersForResolver(BuildModelTypeMembers(modelMemberExpression));
			}
		}
	}
}