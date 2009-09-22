using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public abstract class AutoFormMapper<TModel, TForm> : Mapper<TModel, TForm> where TModel : PersistentObject, new()
	{
		protected AutoFormMapper(IRepository<TModel> repository) : base(repository) {}

		public override K Map<T, K>(T model)
		{
			return Mapper.Map<T, K>(model);
		}
	}
}