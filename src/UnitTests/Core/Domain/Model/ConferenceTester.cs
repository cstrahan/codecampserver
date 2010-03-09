using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class ConferenceTester : PersistentObjectTester
	{
		protected override PersistentObject CreatePersisentObject()
		{
			return new Conference();
		}
	}
}