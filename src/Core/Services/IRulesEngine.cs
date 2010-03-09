using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeCampServer.Core.Services
{
	public interface IRulesEngine
	{
		ICanSucceed Process(object message);
	}

	public interface ICanSucceed
	{
		bool Successful { get; }
		IEnumerable<ErrorMessage> Errors { get; }
		T Result<T>();
	}

	public class ErrorMessage
	{
		public LambdaExpression InvalidProperty { get; set; }
		public string Message { get; set; }
	}
}