using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Services;
using Tarantino.RulesEngine;
using ErrorMessage = CodeCampServer.Core.Services.ErrorMessage;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class RulesEngine : IRulesEngine
	{
		private readonly global::CommandProcessor.IRulesEngine _rulesEngine;

		public RulesEngine(global::CommandProcessor.IRulesEngine rulesEngine)
		{
			_rulesEngine = rulesEngine;
		}

		public ICanSucceed Process(object message)
		{
			ExecutionResult result = _rulesEngine.Process(message);
			return
				new SuccessResult(
					Messages(result),
					Results(result.ReturnItems)) {Successful = result.Successful};
		}

		private Dictionary<Type, object> Results(GenericItemDictionary result)
		{
			var returnVal = new Dictionary<Type, object>();
			foreach (KeyValuePair<Type, object> executionResult in result)
			{
				returnVal.Add(executionResult.Key, executionResult.Value);
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
}