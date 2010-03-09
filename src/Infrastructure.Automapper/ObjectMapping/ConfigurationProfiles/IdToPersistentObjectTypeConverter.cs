using AutoMapper;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public class IdToPersistentObjectTypeConverter<T, K> : ITypeConverter<T, K> where K : PersistentObject
	{
		private readonly IRepository<K> _repository;

		public IdToPersistentObjectTypeConverter(IRepository<K> repository)
		{
			_repository = repository;
		}

		public K Convert(T source)
		{
			return _repository.GetById(source);
		}
	}
}