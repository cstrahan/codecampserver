using Gallio.Framework;
using Gallio.Model.Logging;

namespace RegressionTests
{
    public class ExecutionSteps
    {
        public static TestLogStreamWriter Log
        {
            get { return TestLog.Writer["Execution Steps"]; }
        }
    }
}