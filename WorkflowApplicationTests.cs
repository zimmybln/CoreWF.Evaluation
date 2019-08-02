using CoreWf;
using CoreWF.Evaluation.Activities;
using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace CoreWF.Evaluation.Tests
{
    public class WorkflowApplicationTests
    {
        private readonly ITestOutputHelper output;

        public WorkflowApplicationTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void OutputEqualsInput()
        {
            string value = "Das ist ein Test";

            SayHelloActivity activity = new SayHelloActivity()
            {
                Message = new CoreWf.InArgument<string>(context => value)
            };

            WorkflowApplication workflowApplication = new WorkflowApplication(activity);
            

            string result = null;

            ManualResetEvent workflowDone = new ManualResetEvent(false);

            workflowApplication.Completed += delegate(WorkflowApplicationCompletedEventArgs args)
            {
                result = args.Outputs["Result"].ToString();
                workflowDone.Set();
            };

            workflowApplication.Aborted += delegate(WorkflowApplicationAbortedEventArgs args)
            {
                workflowDone.Set();
            };

            // Ausführen und warten
            workflowApplication.Run();
            workflowDone.WaitOne();
            
            Assert.Equal(value, result);

            
        }
    }
}
