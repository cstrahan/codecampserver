using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.Automapper.ObjectMapping.ConfigurationProfiles
{
	public class GuidIdToSponsorTypeConverter : ITypeConverter<Guid, Sponsor>
	{
		private ISponsorRepository _repository;

		public GuidIdToSponsorTypeConverter(ISponsorRepository repository)
		{
			_repository = repository;
		}

		public Sponsor Convert(Guid source)
		{
			return _repository.GetById(source);
		}
	}
}