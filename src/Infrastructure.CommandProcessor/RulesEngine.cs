using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Services;
using MvcContrib.CommandProcessor;
using ErrorMessage=CodeCampServer.Core.Services.ErrorMessage;
using IRulesEngine=CodeCampServer.Core.Services.IRulesEngine;

namespace CodeCampServer.Infrastructure.CommandProcessor
{
	public class RulesEngine : IRulesEngine
	{
		private readonly MvcContrib.CommandProcessor.IRulesEngine _rulesEngine;

		public RulesEngine(MvcContrib.CommandProcessor.IRulesEngine rulesEngine)
		{
			_rulesEngine = rulesEngine;
		}

		public ICanSucceed Process(object message)
		{
			var result = _rulesEngine.Process(message);
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