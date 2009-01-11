using System;
using System.Linq.Expressions;

namespace CodeCampServer.Infrastructure.AutoMap
{
	public interface IMappingExpression<TSource, TDestination>
	{
		IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember,
		                                                    Action<IFormattingExpression<TSource>> memberOptions);

		void ForAllMembers(Action<IFormattingExpression<TSource>> memberOptions);

		IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>() where TOtherSource : TSource
			where TOtherDestination : TDestination;

		IMappingExpression<TSource, TDestination> WithProfile(string profileName);
	}

	public interface IFormattingExpression<TSource>
	{
		void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
		void AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
		void AddFormatter(Type valueFormatterType);
		void AddFormatter(IValueFormatter formatter);
		void FormatNullValueAs(string nullSubstitute);
		IResolutionExpression<TSource> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver;
		IResolutionExpression<TSource> ResolveUsing(Type valueResolverType);
		IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver);
		void MapFrom(Expression<Func<TSource, object>> sourceMember);
		void Ignore();
	}

	public interface IResolutionExpression<TSource>
	{
		void FromMember(Expression<Func<TSource, object>> sourceMember);
	}
}