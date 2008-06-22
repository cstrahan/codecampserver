using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class UserSessionStub : UserSession
	{
		private readonly Person _person;

		public UserSessionStub(Person person) : base(null, null)
		{
			_person = person;
		}

		public override Person GetLoggedInPerson()
		{
			return _person;
		}
	}
}