using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class AttendeeTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Attendee();
        }

    	[Test]
    	public void ShouldGetProperName()
    	{
    		Attendee attendee = new Attendee();
    		attendee.Contact.FirstName = "Homey";
    		attendee.Contact.LastName = "Simpsoy";
			Assert.That(attendee.GetName(), Is.EqualTo("Homey Simpsoy"));
    	}
    }
}