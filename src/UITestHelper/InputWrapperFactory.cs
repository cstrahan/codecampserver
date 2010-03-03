using System;
using System.Collections.Generic;

namespace UITestHelper
{
    public class InputWrapperFactory : List<IInputWrapperFactory>
    {
        public static Func<IList<IInputWrapperFactory>> Factory = () => new InputWrapperFactory();

        public InputWrapperFactory()
        {
            Add(new TextInputWrapperFactory());
        }
    }
}