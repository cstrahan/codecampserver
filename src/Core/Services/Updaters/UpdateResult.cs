using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Services.Updaters
{
	public class UpdateResult<TModel, TMessage> where TModel : PersistentObject
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

		private readonly IDictionary<string, string[]> _messages = new Dictionary<string, string[]>();


		public UpdateResult<TModel, TMessage> WithMessage(Expression<Func<TMessage, object>> messageExpression, string message)
		{
			string key = UINameHelper.BuildNameFrom(messageExpression);

			if (_messages.ContainsKey(key))
			{
				var strings = new List<string>(_messages[key]) {message};
				_messages[key] = strings.ToArray();
			}
			else
			{
				_messages.Add(key, new[] {message});
			}

			return this;
		}

		public string[] GetMessages(string key)
		{
			return _messages[key];
		}

		public string[] GetMessages(Expression<Func<TMessage, object>> messageExpression)
		{
			return GetMessages(UINameHelper.BuildNameFrom(messageExpression));
		}

		public IDictionary<string, string[]> GetAllMessages()
		{
			return new Dictionary<string, string[]>(_messages);
		}
	}
}