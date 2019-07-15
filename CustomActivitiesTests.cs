using CoreWf;
using CoreWF.Evaluation.Activities;
using System;
using Xunit;

namespace CoreWF.Evaluation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            SayHelloActivity activity = new SayHelloActivity()
            {
                Message = new CoreWf.InArgument<string>(context => "das ist ein Test")
            };

            WorkflowApplication workflowApplication = new WorkflowApplication(activity);

            workflowApplication.Run();
        }
    }
}
