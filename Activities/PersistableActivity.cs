using System;
using System.Collections.Generic;
using System.Text;
using CoreWf;

namespace CoreWF.Evaluation.Tests.Activities
{
    public class PersistableActivity : NativeActivity<bool>
    {
        protected override bool CanInduceIdle { get; } = true;

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark("Name", OnResumeBookmark, BookmarkOptions.None);
        }

        private void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object value)
        {
            
        }
    }
}
