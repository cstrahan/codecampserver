using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.BusinessRules;
using CodeCampServer.Infrastructure.ObjectMapping;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using CommandProcessor;
using Microsoft.Practices.ServiceLocation;
using Rhino.Mocks;
using StructureMap;
using Tarantino.RulesEngine;
using IUnitOfWork=CodeCampServer.Infrastructure.DataAccess.IUnitOfWork;

namespace CodeCampServer.IntegrationTests.UI.Subcutaneous
{
	public class SubcutaneousTest<TForm> : DataTestBase where TForm : new()
	{
		public override void Setup()
		{
			ObjectFactory.ResetDefaults();
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ServiceLocator.SetLocatorProvider(() => new SubcutaneousTestServiceLocator(UnitOfWork));
			var rulesEngine = new RulesEngine();
			RulesEngine.MessageProcessorFactory = new MessageProcessorFactory();
			rulesEngine.Initialize(typeof (DeleteMeetingMessageConfiguration).Assembly,
			                       new RulesEngineConfiguration.CcsMessageMapper());

			base.Setup();
		}

		private RulesEngine GetMasterHandler()
		{
			return GetInstance<RulesEngine>();
		}

		protected ExecutionResult ProcessForm(TForm form)
		{
			ExecutionResult result = GetMasterHandler().Process(form, typeof (TForm));

			if (result.Successful)
				UnitOfWork.Commit();

			return result;
		}

		protected static void SetupClock(DateTime today)
		{
			var clock = S<ISystemClock>();
			clock.Stub(x => x.Now()).Return(today);
			ObjectFactory.Inject(clock);
		}

		protected string Date(int year, int month, int day)
		{
			return FormatDate(new DateTime(year, month, day));
		}

		protected string Rand()
		{
			return Guid.NewGuid().ToString().Substring(0, 7);
		}

		protected T FormatDateTime<T>(DateTime date) where T : IDateAndTime, new()
		{
			var form = new T
			           	{
			           		Date = Format.Date(date),
			           		Hour = date.Hour.ToString(),
			           		Minute = date.Minute.ToString()
			           	};

			return form;
		}

		protected string FormatDate(DateTime date)
		{
			return Format.Date(date);
		}

		internal class SubcutaneousTestServiceLocator : IServiceLocator
		{
			private readonly IUnitOfWork _unitOfWork;

			public SubcutaneousTestServiceLocator(IUnitOfWork unitOfWork)
			{
				_unitOfWork = unitOfWork;
			}

			public object GetService(Type serviceType)
			{
				throw new NotImplementedException();
			}

			public object GetInstance(Type serviceType)
			{
				return ObjectFactory.With(_unitOfWork).GetInstance(serviceType);
			}

			public object GetInstance(Type serviceType, string key)
			{
				throw new NotImplementedException();
			}

			public IEnumerable<object> GetAllInstances(Type serviceType)
			{
				return ObjectFactory.With(_unitOfWork).GetAllInstances(serviceType).Cast<object>();
			}

			public TService GetInstance<TService>()
			{
				return ObjectFactory.With(_unitOfWork).GetInstance<TService>();
			}

			public TService GetInstance<TService>(string key)
			{
				throw new NotImplementedException();
			}

			public IEnumerable<TService> GetAllInstances<TService>()
			{
				throw new NotImplementedException();
			}
		}
	}
}