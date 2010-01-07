using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;

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
			User[] users = _repository.GetAll();
			return new SelectList(users, "Id", "Name", selected);
		}
	}
}