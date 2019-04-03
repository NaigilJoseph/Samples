using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PInvokePerformance
{
    public class PerformanceTest
    {
        [DllImport("TraditionalAPI.dll")]
        private static extern void TA_IncrementCounter();

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_CalculateSquareRoot(double value);

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_DotProduct(double[] threeTuple1, double[] threeTuple2);

        [DllImport("TraditionalAPI.dll")]
        private static extern void TA_Test1(ulong count);

        [DllImport("TraditionalAPI.dll")]
        private static extern void TA_Test2(ulong count);

        [DllImport("TraditionalAPI.dll")]
        private static extern void TA_Test3(ulong count);

        public void RunTests()
        {
            //  Run the unmanged tests.
            stopwatch.Restart();
            TA_Test1(testCount);
            stopwatch.Stop();
            Unmanaged_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            TA_Test2(testCount);
            stopwatch.Stop();
            Unmanaged_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            TA_Test3(testCount);
            stopwatch.Stop();
            Unmanaged_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;

            //  Create the managed interface.
            ManagedInterface.ManagedInterface managedInterface = new ManagedInterface.ManagedInterface();

            //  Run the tests through the interface.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
                managedInterface.IncrementCounter();
            stopwatch.Stop();
            ManagedInterface_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                double d = managedInterface.CalculateSquareRoot((double)i);
            }
            stopwatch.Stop();
            ManagedInterface_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                double[] threeTuple1 = new double[] { i, i, i };
                double[] threeTuple2 = new double[] { i, i, i };
                double d = managedInterface.DotProduct(threeTuple1, threeTuple2);
            }
            stopwatch.Stop();
            ManagedInterface_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;

            //  Run the tests through the pinvoke.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
                TA_IncrementCounter();
            stopwatch.Stop();
            PInvoke_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                double d = TA_CalculateSquareRoot((double)i);
            }
            stopwatch.Stop();
            PInvoke_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                double[] threeTuple1 = new double[] { i, i, i };
                double[] threeTuple2 = new double[] { i, i, i };
                double d = TA_DotProduct(threeTuple1, threeTuple2);
            }
            stopwatch.Stop();
            PInvoke_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;
        }

        private ulong testCount = 1000000;

        public ulong TestCount
        {
            get { return testCount; }
            set { testCount = value; }
        }

        private Stopwatch stopwatch = new Stopwatch();

        public double Unmanaged_Test1_Time { get; set; }
        public double Unmanaged_Test2_Time { get; set; }
        public double Unmanaged_Test3_Time { get; set; }

        public double ManagedInterface_Test1_Time { get; set; }
        public double ManagedInterface_Test2_Time { get; set; }
        public double ManagedInterface_Test3_Time { get; set; }

        public double PInvoke_Test1_Time { get; set; }
        public double PInvoke_Test2_Time { get; set; }
        public double PInvoke_Test3_Time { get; set; }
    }
}
