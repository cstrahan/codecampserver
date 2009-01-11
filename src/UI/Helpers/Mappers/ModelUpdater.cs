using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Helpers.Mappers;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public abstract class ModelUpdater<TModel, TMessage> : IModelUpdater<TModel, TMessage>
		where TModel : PersistentObject, new()
	{
		protected abstract IRepository<TModel> Repository { get; }

		public virtual UpdateResult<TModel, TMessage> UpdateFromMessage(TMessage message)
		{
			TModel model = GetModel(message);

			UpdateResult<TModel, TMessage> result = PreValidate(message);

			if (!result.Successful)
			{
				return result;
			}

			UpdateModel(message, model);

			result = PostValidate(message, model);

			if (!result.Successful)
			{
				return result;
			}

			SaveModel(model);

			return Success(model);
		}

		protected abstract Guid GetIdFromMessage(TMessage message);

		protected virtual TModel GetModel(TMessage message)
		{
			return Repository.GetById(GetIdFromMessage(message)) ?? new TModel();
		}

		protected virtual UpdateResult<TModel, TMessage> PreValidate(TMessage message)
		{
			return Success();
		}

		protected abstract void UpdateModel(TMessage message, TModel model);

		protected virtual UpdateResult<TModel, TMessage> PostValidate(TMessage message, TModel model)
		{
			return Success();
		}

		protected virtual void SaveModel(TModel model)
		{
			Repository.Save(model);
		}

		public static UpdateResult<TModel, TMessage> Success()
		{
			return new UpdateResult<TModel, TMessage>(true);
		}

		public static UpdateResult<TModel, TMessage> Success(TModel model)
		{
			return new UpdateResult<TModel, TMessage>(true, model);
		}

		public static UpdateResult<TModel, TMessage> Fail()
		{
			return new UpdateResult<TModel, TMessage>(false);
		}

		protected static DateTime? ToDateTime(string value)
		{
			DateTime result;
			bool success = DateTime.TryParse(value, out result);
			if (!success)
			{
				return null;
			}
			return result;
		}

		protected static int ToInt32(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(int);
			}
			return Convert.ToInt32(value);
		}
	}
}