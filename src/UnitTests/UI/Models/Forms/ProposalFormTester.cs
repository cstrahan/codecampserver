using System;
using CodeCampServer.UI.Models.Forms;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.UI.Models.Forms
{
	[TestFixture]
	public class ProposalFormTester
	{
		[Test]
		public void When_id_is_empty_Should_be_transient()
		{
			new ProposalForm{Id = Guid.Empty}.IsTransient().ShouldBeTrue();
		}

		[Test]
		public void When_id_is_not_empty_Should_not_be_transient()
		{
			new ProposalForm { Id = Guid.NewGuid() }.IsTransient().ShouldBeFalse();
		}

	}
}