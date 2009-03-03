using System;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public class InputBuilderFactory : IInputBuilderFactory
	{
		private readonly IInputBuilder[] _inputBuilders;

		public InputBuilderFactory(IInputBuilder[] inputBuilders)
		{
			_inputBuilders = inputBuilders;
		}

		public IInputBuilder FindInputBuilderFor(IInputSpecification inputSpecification)
		{
			foreach (IInputBuilder inputCreator in _inputBuilders)
			{
				Type type = inputSpecification.InputBuilderType;
				if (type != null && type.IsAssignableFrom(inputCreator.GetType()))
				{
					return inputCreator;
				}
			}

			foreach (var inputBuilder in _inputBuilders)
			{
			if (inputBuilder.IsSatisfiedBy(inputSpecification))
				{
					return inputBuilder;
				}
			}

			throw new ArgumentException("No input builder found for given input specification.", "inputSpecification");
		}
	}
}