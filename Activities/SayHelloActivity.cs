using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CoreWf;

namespace CoreWF.Evaluation.Activities
{
    public class SayHelloActivity : CodeActivity<string>
    {
        public InArgument<string> Message { get; set; }
        
        protected override string Execute(CodeActivityContext context)
        {
            string message = Message.Get(context);

            return message;
        }
    }
}
