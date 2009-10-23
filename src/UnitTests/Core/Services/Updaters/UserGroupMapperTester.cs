using CodeCampServer.Core.Domain;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class UserGroupMapperTester : TestBase
	{
		[Test]
		public void Should_delegate_to_the_type_converter()
		{
			var form = S<UserGroupInput>();

			var userGroupRepository = M<IUserGroupRepository>();
			userGroupRepository.Stub(x => x.GetById(form.Id)).Return(null);

			var converter = S<IUserGroupInputToUserGroupTypeConverter>();

			var mapper = new UserGroupMapper(userGroupRepository, converter);
			mapper.Map(form);
			converter.AssertWasCalled(typeConverter => typeConverter.UpdateUserGroupFromInput(null, null),
			                          options => options.IgnoreArguments());
		}
	}
}