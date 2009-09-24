using AutoMapper;

namespace CodeCampServer.UI.Views
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());
		}
	}
}