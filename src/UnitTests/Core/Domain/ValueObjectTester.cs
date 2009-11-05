using System;
using CodeCampServer.Core.Domain;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain
{
	public abstract class ValueObjectTester<T> where T : ValueObject<T>
	{
		protected abstract ValueObject<T> CreateValueObject();
		protected abstract ValueObject<T> CreateDifferentValueObject();

		[Test]
		public void Should_properly_report_equality_with_default()
		{
			T stub = default(T);
			T stub2 = default(T);

			stub.ShouldEqual(stub2);
		}

		[Test]
		public void Should_properly_report_equality_with_hydrated_props()
		{
			ValueObject<T> stub = CreateValueObject();
			ValueObject<T> stub2 = CreateValueObject();

			stub.ShouldEqual(stub2);
			stub.ShouldNotBeTheSameAs(stub2);
		}

		[Test]
		public void Should_properly_report_equality_when_same_reference()
		{
			ValueObject<T> stub = CreateValueObject();

			stub.ShouldEqual(stub);
			stub.ShouldBeTheSameAs(stub);
		}

		[Test]
		public void Should_properly_report_nonequality()
		{
			ValueObject<T> stub = CreateValueObject();
			ValueObject<T> stub2 = CreateDifferentValueObject();

			stub.ShouldNotEqual(stub2);
			stub.ShouldNotBeTheSameAs(stub2);
		}

		[Test]
		public void Should_properly_report_nonequality_when_not_same_type()
		{
			var stub = new Stub();
			ValueObject<T> stub2 = CreateValueObject();

			stub2.Equals(stub).ShouldBeFalse();
			stub.Equals(stub2).ShouldBeFalse();
		}

		[Test]
		public void Should_report_get_hash_code_different_if_different_values()
		{
			ValueObject<T> stub = CreateValueObject();
			ValueObject<T> stub2 = CreateDifferentValueObject();

			stub.GetHashCode().ShouldNotEqual(stub2.GetHashCode());
		}

		[Test]
		public void Should_report_get_hash_code_same_if_same_values()
		{
			ValueObject<T> stub = CreateValueObject();
			ValueObject<T> stub2 = CreateValueObject();

			stub.GetHashCode().ShouldEqual(stub2.GetHashCode());
		}

		[Test]
		public void Should_report_not_equal_if_obj_is_null()
		{
			ValueObject<T> stub = CreateValueObject();
			(stub.Equals(null)).ShouldBeFalse();
		}

		[Test]
		public void Should_compare_null_attributes_to_non_null_attributes()
		{
			var stub1 = new ValueObjectStub();
			var stub2 = new ValueObjectStub();

			stub1.Address = "123 Main";

			stub1.ShouldNotEqual(stub2);
			stub2.ShouldNotEqual(stub1);
		}

		[Test]
		public void Should_compare_null_dates_to_non_null_attributes()
		{
			var stub1 = new ValueObjectStub();
			var stub2 = new ValueObjectStub();

			stub1.Date = DateTime.Now;

			stub1.ShouldNotEqual(stub2);
			stub2.ShouldNotEqual(stub1);
		}

		protected class Stub {}
	}

	public class ValueObjectStub : ValueObject<ValueObjectStub>
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Address { get; set; }
		public DateTime? Date { get; set; }
	}

	[TestFixture]
	public class ValueObjectStubTester : ValueObjectTester<ValueObjectStub>
	{
		protected override ValueObject<ValueObjectStub> CreateValueObject()
		{
			return new ValueObjectStub {Address = "4", Age = 2, Name = null};
		}

		protected override ValueObject<ValueObjectStub> CreateDifferentValueObject()
		{
			return new ValueObjectStub {Address = "1", Age = 2, Name = null};
		}
	}
}