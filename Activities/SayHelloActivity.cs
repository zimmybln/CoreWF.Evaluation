using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using CoreWf;

namespace CoreWF.Evaluation.Activities
{
    public class SayHelloActivity : CodeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> Name { get; set; }

        protected override string Execute(CodeActivityContext context)
        {
            return $"Hallo {Name.Get(context)}";
        }
    }
}
