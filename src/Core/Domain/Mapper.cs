using System;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public abstract class Mapper<TModel, TMessage> : IMapper<TModel, TMessage> where TModel : PersistentObject, new()
	{
		private readonly IRepository<TModel> _repository;

		protected Mapper(IRepository<TModel> repository)
		{
			_repository = repository;
		}

		public abstract K Map<T, K>(T model);

		public virtual TMessage Map(TModel model)
		{
			return Map<TMessage>(model);
		}

		public TMessage2 Map<TMessage2>(TModel model)
		{
			return Map<TModel, TMessage2>(model);
		}

		public virtual TModel Map(TMessage message)
		{
			TModel model = _repository.GetById(GetIdFromMessage(message)) ?? new TModel();
			MapToModel(message, model);
			return model;
		}

		protected abstract Guid GetIdFromMessage(TMessage message);
		protected abstract void MapToModel(TMessage message, TModel model);

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
	}
}