using System;
using CodeCampServer.Core.Bases;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace Tarantino.UnitTests.Core.Commons.Model
{
	public abstract class PersistentObjectTester
	{
		[Test]
		public void asdfoo()
		{
			object objA = null;
			object objB = null;
			Equals(objA, objB).ShouldBeTrue();

			Equals(Guid.Empty, Guid.Empty).ShouldBeTrue();
			Equals(new int?(), new int?()).ShouldBeTrue();
			Equals(0, default(Int32)).ShouldBeTrue();
			Equals(new int?(), new int?(3)).ShouldBeFalse();
			Equals(this, this.GetHashCode()).ShouldBeFalse();
		}

		[Test]
		public void CanAssignAndReadId()
		{
			PersistentObject persistentObject = CreatePersisentObject();

			Assert.AreEqual(Guid.Empty, persistentObject.Id);
			Guid id = Guid.NewGuid();
			persistentObject.Id = id;
			Assert.AreEqual(id, persistentObject.Id);
		}

		[Test]
		public void CorrectlyIdentifiesWhenObjectIsPersistent()
		{
			PersistentObject persistentObject = CreatePersisentObject();
			Assert.AreEqual(false, persistentObject.IsPersistent);

			persistentObject.Id = Guid.NewGuid();
			Assert.AreEqual(true, persistentObject.IsPersistent);
		}

		[Test]
		public virtual void CorrectlyDeterminesTwoNonPersistentObjectsAreNotEqual()
		{
			PersistentObject persistable1 = CreatePersisentObject();
			PersistentObject persistable2 = CreatePersisentObject();

			Assert.AreNotEqual(persistable1, persistable2);
		}

		[Test]
		public void CorrectlyDeterminesTwoPersistentObjectsWithDifferentIdsAreNotEqual()
		{
			PersistentObject persistable1 = CreatePersisentObject();
			PersistentObject persistable2 = CreatePersisentObject();

			persistable1.Id = Guid.NewGuid();
			persistable2.Id = Guid.NewGuid();

			Assert.AreNotEqual(persistable1, persistable2);
		}

		[Test]
		public void CorrectlyDeterminesTwoPeristentObjectsWithTheSameIdAreEqual()
		{
			PersistentObject persistable1 = CreatePersisentObject();
			PersistentObject persistable2 = CreatePersisentObject();

			Guid id = Guid.NewGuid();
			persistable1.Id = id;
			persistable2.Id = id;

			Assert.AreEqual(persistable1, persistable2);
		}

		[Test]
		public void CorrectlyDeterminesTwoNonPeristentObjectsWithTheReferenceAreEqual()
		{
			PersistentObject persistable1 = CreatePersisentObject();
			PersistentObject persistable2 = persistable1;

			Assert.AreEqual(persistable1, persistable2);
		}

		[Test]
		public void CorrectlyReturnsHashcodeForNonPersistentPersistable()
		{
			PersistentObject persistentObject = CreatePersisentObject();
			Assert.AreNotEqual(Guid.Empty.GetHashCode(), persistentObject.GetHashCode());
		}

		[Test]
		public void CorrectlyReturnsHashcodeForPersistentPersistable()
		{
			Guid id = Guid.NewGuid();

			PersistentObject persistentObject = CreatePersisentObject();
			persistentObject.Id = id;

			Assert.AreEqual(id.GetHashCode(), persistentObject.GetHashCode());
		}

		protected abstract PersistentObject CreatePersisentObject();
	}
}