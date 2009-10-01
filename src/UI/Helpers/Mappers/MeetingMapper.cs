using AutoMapper;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class MeetingMapper : IMeetingMapper
	{
		private readonly IMappingEngine _mappingEngine;

		public MeetingMapper(IMappingEngine mappingEngine)
		{
			_mappingEngine = mappingEngine;
		}

		public MeetingInput Map(Meeting model)
		{
			return _mappingEngine.Map<Meeting, MeetingInput>(model);
		}

		public TMessage2 Map<TMessage2>(Meeting model)
		{
			return _mappingEngine.Map<Meeting, TMessage2>(model);
		}

		public Meeting Map(MeetingInput message)
		{
			return _mappingEngine.Map<MeetingInput, Meeting>(message);
		}
	}
}