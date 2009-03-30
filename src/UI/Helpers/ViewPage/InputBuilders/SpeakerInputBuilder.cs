using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class SpeakerInputBuilder : PersistentObjectInputBuilder<Speaker>
	{
		private readonly ISpeakerRepository _repository;

		public SpeakerInputBuilder(ISpeakerRepository repository)
		{
			_repository = repository;
		}


		protected override Expression<Func<Speaker, string>> GetDisplayPropertyExpression()
		{
			return t => t.FirstName + " " + t.LastName;
		}

		protected override Speaker[] GetList()
		{
			return _repository.GetAllForConference(base.InputSpecification.Helper.ViewData.Get<Conference>());
		}
	}
}