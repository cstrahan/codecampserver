using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Attributes;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests
{
	[TestFixture]
	public class UINameHelperTester
	{
		[Test]
		public void should_build_name_from_typed_expression()
		{
			Expression<Func<ExampleForm, object>> expression = f => f.DrugTestId;
			UINameHelper.BuildNameFrom(expression).ShouldEqual("DrugTestId");
		}

		[Test]
		public void Should_build_name_from_basic_expression()
		{
			Expression<Func<ExampleForm, object>> expression = f => f.DrugTestId;
			UINameHelper.BuildNameFrom(expression).ShouldEqual("DrugTestId");
		}

		[Test]
		public void Should_build_name_from_indexed_expression()
		{
			Expression<Func<ExampleForm, object>> expression = f => f.DrugTestDrugTestResults[5].SubstanceTested;
			UINameHelper.BuildNameFrom(expression).ShouldEqual("DrugTestDrugTestResults[5].SubstanceTested");
		}


		[Test]
		public void Should_extract_index_values_from_expressions()
		{
			Expression<Func<ExampleForm, object>> expression1 = f => f.DrugTestId;
			Expression<Func<ExampleForm, object>> expression2 = f => f.DrugTestDrugTestResults[5].SubstanceTested;

			UINameHelper.ExtractIndexValueFrom(expression1).ShouldBeNull();
			UINameHelper.ExtractIndexValueFrom(expression2).ShouldEqual(5);
		}


		public class SomeForm
		{
			public object GetSomeValue()
			{
				return null;
			}
		}
	}

	[Serializable]
	public class ExampleForm
	{
		[Hidden]
		public Guid DrugTestId { get; set; }

		[Label("Test Date")]
		public string DrugTestTestDate { get; set; }

		[Hidden]
		public Guid JuvenileId { get; set; }

		[ShowAsRequired]
		[Label("Other Location")]
		public string DrugTestTestLocationOtherDescription { set; get; }

		[Label("Administered By")]
		public Guid? DrugTestAdministeredById { get; set; }

		[Label("Administered")]
		public virtual bool? DrugTestAdministered { get; set; }

		[Label("Witnessed By")]
		public Guid? DrugTestWitnessedById { get; set; }


		[Label("Drug Test Results")]
		public Foo[] DrugTestDrugTestResults { get; set; }
	}

	public class Foo
	{
		public object SubstanceTested { get; set; }
	}
}