using System;
using AutoMapper;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain;

namespace CodeCampServer.Infrastructure.ObjectMapping.TypeConverters
{
	public class GuidIdToEntityConverter<TEntity> : ITypeConverter<Guid, TEntity>
		where TEntity : PersistentObject
	{
		private readonly IRepository<TEntity> _repository;

		public GuidIdToEntityConverter(IRepository<TEntity> repository)
		{
			_repository = repository;
		}

		public TEntity Convert(Guid source)
		{
			var entityNotSpecified = source == Guid.Empty;
			TEntity entity = entityNotSpecified ? null : _repository.GetById(source);
			return entity;
		}
	}
}