using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CoreWf;
using CoreWF.Evaluation.Activities;
using Xunit;

namespace CoreWF.Evaluation.Tests
{
    public class WorkflowInvokerTests
    {
        [Fact]
        public void OutputEqualsInput()
        {
            string value = "Das ist ein Test";

            SayHelloActivity activity = new SayHelloActivity()
            {
                Message = new InArgument<string>(context => value)
            };

            var result = WorkflowInvoker.Invoke(activity);

            Assert.Equal(value, result);


        }
    }
}
