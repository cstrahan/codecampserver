using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.ObjectMapping.TypeConverters
{
	public class IdToEntityConverter<TEntity> : ITypeConverter<Guid, TEntity>
		where TEntity : PersistentObject
	{
		private readonly IRepository<TEntity> _repository;

		public IdToEntityConverter(IRepository<TEntity> repository)
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