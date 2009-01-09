using System;
using System.Globalization;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Binders;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI
{
	public abstract class KeyedModelBinderTester<TEntity, TRepository> : BinderTester
		where TRepository : class, IKeyedRepository<TEntity>
		where TEntity : KeyedObject, new()
	{
		public abstract Type return_model_binder();

		[Test]
		public void Can_instantiate()
		{
			Activator.CreateInstance(return_model_binder(), MockRepository.GenerateMock<TRepository>());
		}

		[Test]
		public void Should_get_entity_from_repository_by_key_on_query_string()
		{
			var repository = MockRepository.GenerateMock<TRepository>();
			var entity = new TEntity();
			repository.Stub(r => r.GetByKey("key")).Return(entity);
			var binder = new KeyedModelBinder<TEntity, TRepository>(repository);
			ControllerContext controllerContext1 = GetControllerContext("foo", "key"); //capitalize
			ControllerContext controllerContext2 = GetControllerContext("bar", "key"); //lowercase

			ModelBinderResult result = binder.BindModel(new ModelBindingContext(controllerContext1,
			                                                                    new DefaultValueProvider(controllerContext1),
			                                                                    typeof (TEntity), "foo", null,
			                                                                    new ModelStateDictionary(), null));
			Assert.That(result.Value, Is.EqualTo(entity));

			result = binder.BindModel(new ModelBindingContext(controllerContext2,
			                                                  new DefaultValueProvider(controllerContext2),
			                                                  typeof (TEntity), "bar", null,
			                                                  new ModelStateDictionary(), null));
			Assert.That(result.Value, Is.EqualTo(entity));
		}

		[Test, ExpectedException(ExceptionType = typeof (ApplicationException),
			ExpectedMessage = "Unable to locate a valid value for query string parameter 'foo'")]
		public void Should_throw_error_when_query_string_parameter_not_found()
		{
			const string badParameter = "Bad Value";

			ControllerContext controllerContext = GetControllerContext("foo", badParameter);
			var valueProvider = new DefaultValueProvider(controllerContext);

			var context = new ModelBindingContext(controllerContext,
			                                      valueProvider, typeof (TEntity), "foo", null,
			                                      new ModelStateDictionary(), null);

			var binder = new KeyedModelBinder<TEntity, TRepository>(null);
			binder.BindModel(context);
		}

		[Test]
		public void Should_return_null_when_query_string_parameter_value_is_null()
		{
			const string badParameter = "";
			ControllerContext controllerContext = GetControllerContext("foo", badParameter);
			var valueProvider = MockRepository.GenerateMock<IValueProvider>();
			valueProvider.Stub(v => v.GetValue("foo")).Return(new ValueProviderResult(null, "", CultureInfo.CurrentCulture));

			var context = new ModelBindingContext(controllerContext, valueProvider, typeof (TEntity), "foo", null,
			                                      new ModelStateDictionary(), null);

			var binder = new KeyedModelBinder<TEntity, TRepository>(null);
			ModelBinderResult binderResult = binder.BindModel(context);
			binderResult.Value.ShouldBeNull();
		}
	}

	[TestFixture]
	public class SampleKeyedModelBinderTester : KeyedModelBinderTester<Speaker, ISpeakerRepository>
	{
		public override Type return_model_binder()
		{
			return typeof (KeyedModelBinder<Speaker, ISpeakerRepository>);
		}
	}
}