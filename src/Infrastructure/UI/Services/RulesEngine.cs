using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core;
using CodeCampServer.Core.Services;
using Tarantino.RulesEngine;
using ErrorMessage=CodeCampServer.Core.Services.ErrorMessage;

namespace CodeCampServer.Infrastructure.UI.Services
{
	public class RulesEngine : IRulesEngine
	{
		private readonly CommandProcessor.IRulesEngine _rulesEngine;

		public RulesEngine(CommandProcessor.IRulesEngine rulesEngine)
		{
			_rulesEngine = rulesEngine;
		}

		public ICanSucceed Process(object message)
		{
			ExecutionResult result = _rulesEngine.Process(message);
			return
				new SuccessResult(
					Messages(result),
					Results(result.ReturnItems)){Successful = result.Successful};
		}

		private Dictionary<Type, object> Results(GenericItemDictionary result)
		{
			var returnVal = new Dictionary<Type, object>();
			foreach (KeyValuePair<Type,object> executionResult in result)
			{
				returnVal.Add(executionResult.Key,executionResult.Value);
			}
			return returnVal;
		}

		private List<ErrorMessage> Messages(ExecutionResult result)
		{
			return result.Messages.Select(
				errorMessage =>
				new ErrorMessage {InvalidProperty = errorMessage.IncorrectAttribute, Message = errorMessage.MessageText}).ToList();
		}
	}

	public class SuccessResult : ICanSucceed
	{
		private readonly Dictionary<Type, object> _results;
		private readonly List<ErrorMessage> _errorMessages;

		public SuccessResult(List<ErrorMessage> errorMessages, Dictionary<Type, object> results)
		{
			_errorMessages = errorMessages;
			_results = results;
		}

		public bool Successful { get; set; }

		public IEnumerable<ErrorMessage> Errors
		{
			get { return _errorMessages; }
		}

		public T Result<T>()
		{
			if(_results.ContainsKey(typeof(T)))
				return (T) _results[typeof (T)];
			
			return default(T);
		}
	}
}