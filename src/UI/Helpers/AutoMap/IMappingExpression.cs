using System;
using System.Linq.Expressions;

namespace CodeCampServer.UI.Models.AutoMap
{
	public interface IMappingExpression<TModel, TDto>
	{
		IMappingExpression<TModel, TDto> ForDtoMember(Expression<Func<TDto, object>> dtoMemberExpression, Action<IFormattingExpression<TModel>> memberOptions);
		void ForAllMembers(Action<IFormattingExpression<TModel>> memberOptions);
	}

	public interface IFormattingExpression<TModel>
	{
		void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
		void AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
		void AddFormatter(IValueFormatter formatter);
		void FormatNullValueAs(string nullSubstitute);
		IResolutionExpression<TModel> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver;
		IResolutionExpression<TModel> ResolveUsing(IValueResolver valueResolver);
		void MapFromMember(Expression<Func<TModel, object>> modelMemberExpression);
	}

	public interface IResolutionExpression<TModel>
	{
		void FromModelMember(Expression<Func<TModel, object>> modelMemberExpression);
	}
}