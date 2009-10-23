using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.Infrastructure.UI.Mappers
{
	public class UserGroupMapper : AutoInputMapper<UserGroup, UserGroupInput>, IUserGroupMapper,
	                               ITypeConverter<string, UserGroup>, ITypeConverter<Guid, UserGroup>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupInputToUserGroupTypeConverter _userGroupInputToUserGroupTypeConverter;

		public UserGroupMapper(IUserGroupRepository repository, IUserGroupInputToUserGroupTypeConverter userGroupInputToUserGroupTypeConverter) : base(repository)
		{
			_repository = repository;
			_userGroupInputToUserGroupTypeConverter = userGroupInputToUserGroupTypeConverter;
		}

		protected override Guid GetIdFromMessage(UserGroupInput input)
		{
			return input.Id;
		}

		protected override void MapToModel(UserGroupInput input, UserGroup model)
		{
		 _userGroupInputToUserGroupTypeConverter.UpdateUserGroupFromInput(model, input);
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