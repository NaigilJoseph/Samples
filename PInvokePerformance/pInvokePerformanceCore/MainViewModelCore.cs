using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using System.ComponentModel;

namespace pInvokePerformanceCore
{
    public class MainViewModelCore : ViewModel
    {
        public MainViewModelCore()
        {
            //  Set up the background worker.
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            //  Create the view model command.
            runTestsCommand = new ViewModelCommand(DoRunTests, true);
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //  Set the view model properties.
            PerformanceTestCore test = e.Result as PerformanceTestCore;
            Unmanaged_Test1_Result = test.Unmanaged_Test1_Time;
            Unmanaged_Test2_Result = test.Unmanaged_Test2_Time;
            Unmanaged_Test3_Result = test.Unmanaged_Test3_Time;
            ManagedInteface_Test1_Result = test.ManagedInterface_Test1_Time;
            ManagedInteface_Test2_Result = test.ManagedInterface_Test2_Time;
            ManagedInteface_Test3_Result = test.ManagedInterface_Test3_Time;
            PInvoke_Test1_Result = test.PInvoke_Test1_Time;
            PInvoke_Test2_Result = test.PInvoke_Test2_Time;
            PInvoke_Test3_Result = test.PInvoke_Test3_Time;

            //  We can now run the command again.
            runTestsCommand.CanExecute = true;
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //  Create a performance test, set the iterations.
            PerformanceTestCore test = new PerformanceTestCore();
            test.TestCount = Iterations;

            //  Run the performance test.
            test.RunTests();

            //  Set the result of the worker.
            e.Result = test;
        }

        private void DoRunTests()
        {
            //  The test cannot run again till we finish.
            runTestsCommand.CanExecute = false;

            //  Run the background worker.
            backgroundWorker.RunWorkerAsync();
        }

        private BackgroundWorker backgroundWorker = new BackgroundWorker();


        private NotifyingProperty IterationsProperty =
          new NotifyingProperty("Iterations", typeof(ulong), 1000000UL);

        public ulong Iterations
        {
            get { return (ulong)GetValue(IterationsProperty); }
            set { SetValue(IterationsProperty, value); }
        }


        private NotifyingProperty Unmanaged_Test1_ResultProperty =
          new NotifyingProperty("Unmanaged_Test1_Result", typeof(double), default(double));

        public double Unmanaged_Test1_Result
        {
            get { return (double)GetValue(Unmanaged_Test1_ResultProperty); }
            set { SetValue(Unmanaged_Test1_ResultProperty, value); }
        }

        private NotifyingProperty Unmanaged_Test2_ResultProperty =
          new NotifyingProperty("Unmanaged_Test2_Result", typeof(double), default(double));

        public double Unmanaged_Test2_Result
        {
            get { return (double)GetValue(Unmanaged_Test2_ResultProperty); }
            set { SetValue(Unmanaged_Test2_ResultProperty, value); }
        }

        private NotifyingProperty Unmanaged_Test3_ResultProperty =
          new NotifyingProperty("Unmanaged_Test3_Result", typeof(double), default(double));

        public double Unmanaged_Test3_Result
        {
            get { return (double)GetValue(Unmanaged_Test3_ResultProperty); }
            set { SetValue(Unmanaged_Test3_ResultProperty, value); }
        }

        private NotifyingProperty ManagedInteface_Test1_ResultProperty =
          new NotifyingProperty("ManagedInteface_Test1_Result", typeof(double), default(double));

        public double ManagedInteface_Test1_Result
        {
            get { return (double)GetValue(ManagedInteface_Test1_ResultProperty); }
            set { SetValue(ManagedInteface_Test1_ResultProperty, value); }
        }

        private NotifyingProperty ManagedInteface_Test2_ResultProperty =
          new NotifyingProperty("ManagedInteface_Test2_Result", typeof(double), default(double));

        public double ManagedInteface_Test2_Result
        {
            get { return (double)GetValue(ManagedInteface_Test2_ResultProperty); }
            set { SetValue(ManagedInteface_Test2_ResultProperty, value); }
        }

        private NotifyingProperty ManagedInteface_Test3_ResultProperty =
          new NotifyingProperty("ManagedInteface_Test3_Result", typeof(double), default(double));

        public double ManagedInteface_Test3_Result
        {
            get { return (double)GetValue(ManagedInteface_Test3_ResultProperty); }
            set { SetValue(ManagedInteface_Test3_ResultProperty, value); }
        }

        private NotifyingProperty PInvoke_Test1_ResultProperty =
          new NotifyingProperty("PInvoke_Test1_Result", typeof(double), default(double));

        public double PInvoke_Test1_Result
        {
            get { return (double)GetValue(PInvoke_Test1_ResultProperty); }
            set { SetValue(PInvoke_Test1_ResultProperty, value); }
        }

        private NotifyingProperty PInvoke_Test2_ResultProperty =
          new NotifyingProperty("PInvoke_Test2_Result", typeof(double), default(double));

        public double PInvoke_Test2_Result
        {
            get { return (double)GetValue(PInvoke_Test2_ResultProperty); }
            set { SetValue(PInvoke_Test2_ResultProperty, value); }
        }

        private NotifyingProperty PInvoke_Test3_ResultProperty =
          new NotifyingProperty("PInvoke_Test3_Result", typeof(double), default(double));

        public double PInvoke_Test3_Result
        {
            get { return (double)GetValue(PInvoke_Test3_ResultProperty); }
            set { SetValue(PInvoke_Test3_ResultProperty, value); }
        }



        private ViewModelCommand runTestsCommand;

        public ViewModelCommand RunTestsCommand
        {
            get { return runTestsCommand; }
        }
    }
}
