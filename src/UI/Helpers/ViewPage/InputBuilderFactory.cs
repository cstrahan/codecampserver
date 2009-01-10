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
				if (inputCreator.IsSatisfiedBy(inputSpecification))
				{
					return inputCreator;
				}
			}

			throw new ArgumentException("No input builder found for given input specification.", "inputSpecification");
		}
	}
}