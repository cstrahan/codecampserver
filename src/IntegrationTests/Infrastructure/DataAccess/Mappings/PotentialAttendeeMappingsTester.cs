using System.Data;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class PotentialAttendeeMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_PotentialAttendee()
		{


			var attendee = new Attendee()
			           	{
			           		EmailAddress = "jdoe@abc.com",
			           		FirstName = "sdf",
                            
			           	};

			AssertObjectCanBePersisted(attendee);

		
		}
	}
}