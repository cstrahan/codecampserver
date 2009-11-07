using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IEventRepository : IKeyedRepository<Event>
	{
		Event[] GetAllForUserGroup(UserGroup usergroup);
		Event[] GetFutureForUserGroup(UserGroup usergroup);
		Event[] GetAllFutureEvents();
	}
}