using CoreWF.Evaluation.Activities;
using System;
using System.Diagnostics;
using System.Threading;
using CoreWf;
using CoreWF.Evaluation.Tests.Activities;
using CoreWf.Statements;
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
                Message = new InArgument<string>(context => value)
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

        [Fact]
        public void PersistWorkflow()
        {
            AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs args)
                {
                    Debug.WriteLine(args.Name);
                    return null;
                };

            // Definition der Ausführung
            Sequence sequence = new Sequence()
            {
                Activities =
                {
                    new WriteLine()
                    {
                        Text = "Der Workflow wurde gestartet"
                    },
                    new PersistableActivity(){},
                    new WriteLine()
                    {
                        Text = "Der Workflow wurde beendet"
                    }
                }
            };

            Guid workflowId = Guid.Empty;
            
            #region Speichern des Workflows

            WorkflowApplication workflowApplicationSave = new WorkflowApplication(sequence);

            workflowApplicationSave.InstanceStore = new JsonFileInstanceStore.FileInstanceStore("D:\\Temp");
            
            ManualResetEvent workflowDoneSave = new ManualResetEvent(false);

            workflowApplicationSave.Completed += args => workflowDoneSave.Set();
            workflowApplicationSave.PersistableIdle += args => PersistableIdleAction.Unload;
            workflowApplicationSave.Aborted += args => workflowDoneSave.Set();
            workflowApplicationSave.Unloaded += args => workflowDoneSave.Set();

            workflowId = workflowApplicationSave.Id;
            workflowApplicationSave.Run();
            workflowDoneSave.WaitOne();

            #endregion

            WorkflowApplication workflowApplicationLoad = new WorkflowApplication(sequence);
            workflowApplicationLoad.InstanceStore = new JsonFileInstanceStore.FileInstanceStore("D:\\Temp");

            ManualResetEvent workflowDoneLoad = new ManualResetEvent(false);

            workflowApplicationLoad.Completed += args => workflowDoneLoad.Set();
            workflowApplicationLoad.PersistableIdle += args => PersistableIdleAction.Unload;
            workflowApplicationLoad.Aborted += args => workflowDoneLoad.Set();
            workflowApplicationLoad.Unloaded += args => workflowDoneLoad.Set();

            workflowApplicationLoad.Load(workflowId);
            workflowApplicationLoad.Run();
            workflowDoneLoad.WaitOne();

        }
    }
}
