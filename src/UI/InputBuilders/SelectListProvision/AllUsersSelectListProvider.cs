using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.UI.InputBuilders.SelectListProvision
{
	public class AllUsersSelectListProvider : ISelectListProvider
	{
		private readonly IUserRepository _repository;

		public AllUsersSelectListProvider(IUserRepository repository)
		{
			_repository = repository;
		}

		public SelectList Provide(object selected)
		{
			var users = _repository.GetAll();
			return new SelectList(users, "Id", "Name", selected);
		}
	}
}