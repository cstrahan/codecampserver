using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.RulesEngine;

namespace CodeCampServer.IntegrationTests.UI.Subcutaneous
{
	public static class ExecutionResultAssertionExtensions
	{
		public static void ShouldHaveMessage<TMessage>(this ExecutionResult result,
		                                               Expression<Func<TMessage, object>> expression)
		{
			PropertyInfo targetProperty = CodeCampServer.Core.Common.ReflectionHelper.FindProperty(expression);
			if (!result.Messages.Any(x => CodeCampServer.Core.Common.ReflectionHelper.FindProperty(x.UIAttribute) == targetProperty))
			{
				string failureMessage = result.Messages
					.Select(x => x.UIAttribute + ":" + x.MessageText)
					.WrapEachWith("", "", "\n");
				
				Assert.Fail(string.Format("No message for {0}.  Other messages include:\n{1}", expression, failureMessage));
				return;
			}
		}

		public static void ShouldNotBeSuccessful(this ExecutionResult result)
		{
			Assert.That(result.Successful, Is.False);
		}

		public static void ShouldBeSuccessful(this ExecutionResult result)
		{
			if (!result.Successful)
			{
				string failureMessage = result.Messages
					.Select(x => x.UIAttribute + ":" + x.MessageText)
					.WrapEachWith("", "", "\n");
				Assert.Fail(string.Format("Not successful. Messages include:\n{0}", failureMessage));
			}
		}
	}
}