using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Binders;
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
			ControllerContext controllerContext1 = GetControllerContext("fooKey", "key"); //capitalize
			ControllerContext controllerContext2 = GetControllerContext("barkey", "key"); //lowercase

			ModelBindingContext modelBindingContext1 = CreateBindingContext("fookey", "key");

			object result = binder.BindModel(controllerContext1,
			                                 modelBindingContext1);


			Assert.That(result, Is.EqualTo(entity));

			ModelBindingContext modelBindingContext2 = CreateBindingContext("barkey", "key");
			result = binder.BindModel(controllerContext2, modelBindingContext2);
			Assert.That(result, Is.EqualTo(entity));
		}

		[Test]
		public void Should_throw_error_when_query_string_parameter_not_found()
		{
			const string badParameter = "Bad Value";

			ControllerContext controllerContext = GetControllerContext("foo", badParameter);


			var context = new ModelBindingContext {ModelName = "foo"};

			var binder = new KeyedModelBinder<TEntity, TRepository>(null);
			object model = binder.BindModel(controllerContext, context);
			Assert.That(model, Is.Null);
		}

		[Test]
		public void Should_return_null_when_query_string_parameter_value_is_null()
		{
			const string badParameter = "";
			ControllerContext controllerContext = GetControllerContext("foo", badParameter);

			ModelBindingContext context = CreateBindingContext("foo", "");

			var binder = new KeyedModelBinder<TEntity, TRepository>(null);

			object binderResult = binder.BindModel(controllerContext, context);
			binderResult.ShouldBeNull();
		}

		[Test]
		public void Should_auto_append_id_when_looking_for_querystring_value()
		{
			var repository = MockRepository.GenerateMock<TRepository>();
			var entity = new TEntity();
			repository.Stub(r => r.GetByKey("key")).Return(entity);
			var binder = new KeyedModelBinder<TEntity, TRepository>(repository);
			ControllerContext controllerContext = GetControllerContext("foo", "key");

			ModelBindingContext context = CreateBindingContext("foo", "key");

			object result = binder.BindModel(controllerContext, context);

			Assert.That(result, Is.EqualTo(entity));
		}
	}

	[TestFixture]
	public class SampleKeyedModelBinderTester : KeyedModelBinderTester<Conference, IConferenceRepository>
	{
		public override Type return_model_binder()
		{
			return typeof (KeyedModelBinder<Conference, IConferenceRepository>);
		}
	}
}