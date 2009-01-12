using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class CryptographerTester
	{
		[Test]
		public void Should_hash_password()
		{
			ICryptographer cryptographer = new Cryptographer();

			var hash = cryptographer.GetPasswordHash("pass", "salt");
			Assert.That(hash, Is.EqualTo("cGS5SkKWZQ/PWvQvJaQfXnAAD7FAuqVmI8302iorwl8NtRaPV7Hr2WsQxAc3wacyhZByZfYZrIWygc0vxfQgfQ=="));
		}

		[Test]
		public void Should_create_salt()
		{
			ICryptographer cryptographer = new Cryptographer();

			var salt = cryptographer.CreateSalt();
			Assert.That(salt.Length, Is.EqualTo(88));
		}
	}
}