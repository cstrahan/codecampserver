using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Infrastructure.AutoMap;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public abstract class FormMapper<TModel, TForm> : IMapper<TModel, TForm> where TModel : PersistentObject, new()
	{
		private readonly IRepository<TModel> _repository;

		protected FormMapper(IRepository<TModel> repository)
		{
			_repository = repository;
		}

		public virtual K Map<T, K>(T model)
		{
			return AutoMapper.Map<T, K>(model);
		}

		public virtual TForm Map(TModel model)
		{
			return Map<TModel, TForm>(model);
		}

		public virtual TModel Map(TForm form)
		{
			TModel model = _repository.GetById(GetIdFromMessage(form)) ?? new TModel();
			MapToModel(form, model);
			return model;
		}

		protected abstract Guid GetIdFromMessage(TForm form);
		protected abstract void MapToModel(TForm form, TModel model);

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