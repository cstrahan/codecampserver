using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Services.Updaters
{
	public class UpdateResult<TModel, TMessage>
		where TModel : PersistentObject
	{
		public bool Successful { get; private set; }
		public TModel Model { get; private set; }

		public UpdateResult(bool successful)
			: this(successful, null)
		{
			Successful = successful;
		}

		public UpdateResult(bool successful, TModel model)
		{
			Successful = successful;
			Model = model;
		}

		private readonly IDictionary<string, string> _messages = new Dictionary<string, string>();

		[Obsolete("This property should be refactored to a method that returns an array")]
		public IDictionary<string, string> Messages
		{
			get { return new Dictionary<string, string>(_messages); }
		}

		public UpdateResult<TModel, TMessage> WithMessage(Expression<Func<TMessage, object>> messageExpression, string message)
		{
			string key = UINameHelper.BuildNameFrom(messageExpression);
			_messages.Add(key, message);
			return this;
		}
	}
}