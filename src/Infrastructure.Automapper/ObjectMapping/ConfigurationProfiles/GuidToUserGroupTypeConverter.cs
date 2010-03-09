using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public class GuidToUserGroupTypeConverter :
		ITypeConverter<string, UserGroup>, ITypeConverter<Guid, UserGroup>
	{
		private readonly IUserGroupRepository _repository;

		public GuidToUserGroupTypeConverter(IUserGroupRepository repository)
		{
			_repository = repository;
		}

		public UserGroup Convert(string source)
		{
			return _repository.GetByKey(source);
		}

		public UserGroup Convert(Guid source)
		{
			return _repository.GetById(source);
		}
	}
}