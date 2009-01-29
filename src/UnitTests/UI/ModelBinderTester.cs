using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Binders;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UnitTests.UI
{
	public abstract class ModelBinderTester<TEntity, TRepository> : BinderTester
		where TRepository : class, IRepository<TEntity>
		where TEntity : PersistentObject, new()
	{
		public abstract Type return_model_binder();

		[Test]
		public void Can_instantiate()
		{
			Activator.CreateInstance(return_model_binder(), MockRepository.GenerateMock<TRepository>());
		}

		[Test]
		public void Should_get_entity_from_repository_by_id_on_query_string()
		{
			Guid guid = Guid.NewGuid();
			var repository = MockRepository.GenerateMock<TRepository>();
			var entity = new TEntity();
			repository.Stub(r => r.GetById(guid)).Return(entity);
			var binder = new ModelBinder<TEntity, TRepository>(repository);
			ControllerContext controllerContext1 = GetControllerContext("fooId", guid.ToString()); //capitalize
			ControllerContext controllerContext2 = GetControllerContext("barid", guid.ToString()); //lowercase

			var modelBindingContext = new ModelBindingContext
			                          	{
			                          		ModelType = typeof (TEntity),
			                          		ModelName = "foo",

											ValueProvider = new Dictionary<string, ValueProviderResult>()
			                          	};
			modelBindingContext.ValueProvider.AddKeyAndValue("fooid",guid);
			object result = binder.BindModel(controllerContext1, modelBindingContext);
			Assert.That(result, Is.EqualTo(entity));

			var modelBindingContext2 = new ModelBindingContext
			                           	{
			                           		ModelType = typeof (TEntity),
			                           		ModelName = "bar",
											ValueProvider = new Dictionary<string, ValueProviderResult>()
			                           	};
			modelBindingContext2.ValueProvider.AddKeyAndValue("barid",guid);
			result = binder.BindModel(controllerContext2, modelBindingContext2);
			Assert.That(result, Is.EqualTo(entity));
		}

		[Test]
		public void Should_auto_append_id_when_looking_for_querystring_value()
		{
			Guid guid = Guid.NewGuid();
			var repository = MockRepository.GenerateMock<TRepository>();
			var entity = new TEntity();
			repository.Stub(r => r.GetById(guid)).Return(entity);
			var binder = new ModelBinder<TEntity, TRepository>(repository);
			ControllerContext controllerContext = GetControllerContext("foo", guid.ToString());


			var context = new ModelBindingContext {ModelType = typeof (TEntity), ModelName = "foo",ValueProvider = new Dictionary<string, ValueProviderResult>()};
			context.ValueProvider.AddKeyAndValue("foo",guid);

			var result = binder.BindModel(controllerContext, context);

			Assert.That(result, Is.EqualTo(entity));
		}

		[Test, ExpectedException(ExceptionType = typeof (ApplicationException),
			ExpectedMessage = "Unable to locate a valid value for query string parameter 'foo'")]
		public void Should_throw_error_when_query_string_parameter_not_found()
		{
			const string badParameter = "Bad Value";

			ControllerContext controllerContext = GetControllerContext("foo", badParameter);

			var context = new ModelBindingContext {ModelType = typeof (TEntity), ModelName = "foo"};
			                                      

			var binder = new ModelBinder<TEntity, TRepository>(null);
			binder.BindModel(controllerContext, context);
		}

		[Test]
		public void Should_return_null_when_query_string_parameter_value_is_null()
		{
			const string badParameter = "";
			ControllerContext controllerContext = GetControllerContext("foo", badParameter);
			//var valueProvider = MockRepository.GenerateMock<IValueProvider>();
			//valueProvider.Stub(v => v.GetValue("foo")).Return(new ValueProviderResult(null, "", CultureInfo.CurrentCulture));

			var context = new ModelBindingContext
			              	{ModelName = "foo", ModelType = typeof (TEntity),ValueProvider = new Dictionary<string, ValueProviderResult>()};

			context.ValueProvider.AddKeyAndValue("foo",null);

			var binder = new ModelBinder<TEntity, TRepository>(null);
			object binderResult = binder.BindModel(GetControllerContext("foo", ""), context);
			binderResult.ShouldBeNull();
		}
	}

	[TestFixture]
	public class SampleModelBinderTester : ModelBinderTester<Track, ITrackRepository>
	{
		public override Type return_model_binder()
		{
			return typeof (ModelBinder<Track, ITrackRepository>);
		}
	}

	public static class ValueProviderExtensions
	{
		public static void AddKeyAndValue(this System.Collections.Generic.IDictionary<string,ValueProviderResult> collection,string key, object value)
		{
			collection.Add(key, new ValueProviderResult(value, value==null? "": value.ToString(),null));
		}
	}
}