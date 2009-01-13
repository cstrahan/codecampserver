using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.ViewPage.InputBuilders
{
	public class TimeSlotInputBuilder : PersistentObjectInputBuilder<TimeSlot>
	{
		private readonly ITimeSlotRepository _repository;

		public TimeSlotInputBuilder(ITimeSlotRepository repository)
		{
			_repository = repository;
		}


		protected override Expression<Func<TimeSlot, string>> GetDisplayPropertyExpression()
		{
			return t => t.StartTime + " to " + t.EndTime;
		}

		protected override TimeSlot[] GetList()
		{
			var conference = InputSpecification.Helper.ViewData.Get<Conference>();
			return _repository.GetAllForConference(conference);
		}
	}
}