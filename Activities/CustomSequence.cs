using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using CoreWf;

namespace CoreWF.Evaluation.Tests.Activities
{
    public class CustomSequence : NativeActivity<int>
    {
        int _activityCounter = 0;

        [Browsable(false)]
        public Collection<Activity> Activities { get; set; }

        [Browsable(false)]
        public Collection<Variable> Variables { get; set; }

        public CustomSequence()
        {
            Activities = new Collection<Activity>();
            Variables = new Collection<Variable>();
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            metadata.SetChildrenCollection(Activities);
            metadata.SetVariablesCollection(Variables);

        }
        protected override void Execute(NativeActivityContext context)
        {
            ScheduleActivities(context);
        }

        void ScheduleActivities(NativeActivityContext context)
        {
            if (_activityCounter < Activities.Count)
            {
                context.ScheduleActivity(this.Activities[_activityCounter++],
                    OnActivityCompleted, OnFaulted);
            }
            else
            {
                // Hier sollte man herausbekommen, ob der Workflow bereits beendet worden ist
                context.SetValue(Result, 0);
            }
        }

        private void OnFaulted(NativeActivityFaultContext faultContext, Exception propagatedException, ActivityInstance propagatedFrom)
        {

        }

        void OnActivityCompleted(NativeActivityContext context,
            ActivityInstance completedInstance)
        {

        }
    }
}
