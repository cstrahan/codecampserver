using System;
using CodeCampServer.Core.Domain.Model;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.UnitTests.Core.Commons.Model;

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