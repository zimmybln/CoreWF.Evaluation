using System;
using System.Collections.Generic;
using System.Text;
using CoreWf;

namespace CoreWF.Evaluation.Tests.Activities
{
    public class ActivityWithValidation : CodeActivity
    {
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            //metadata.AddValidationError("");
        }


        protected override void Execute(CodeActivityContext context)
        {
            
        }
    }
}
