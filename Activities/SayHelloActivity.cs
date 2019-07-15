using CoreWf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CoreWF.Evaluation.Activities
{
    public class SayHelloActivity : CodeActivity
    {
        public InArgument<string> Message { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            string message = Message.Get(context);

            Debug.WriteLine(message);
        }
    }
}
