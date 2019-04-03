using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;

namespace PInvokePerformance
{
   // [SuppressUnmanagedCodeSecurity()]
    public class PerformanceTest
    {
        [DllImport("TraditionalAPI.dll")]
        private static extern void TA_IncrementCounter();

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_CalculateSquareRoot(double value);

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_DotProduct(double[] threeTuple1, double[] threeTuple2);

        [DllImport("TraditionalAPI.dll")]
        private static extern uint TA_Test1(double count);

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_Test2(double count);

        [DllImport("TraditionalAPI.dll")]
        private static extern double TA_Test3(double count);

        public void RunTests()
        {
            double dResult = 0;
            uint uResult = 0;
            //  Run the unmanged tests.
            stopwatch.Restart();
            uResult = TA_Test1(testCount);
            stopwatch.Stop();
            Unmanaged_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            dResult = TA_Test2(testCount);
            stopwatch.Stop();
            Unmanaged_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            dResult = TA_Test3(testCount);
            stopwatch.Stop();
            Unmanaged_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;

            //  Create the managed interface.
            ManagedInterface.ManagedInterface managedInterface = new ManagedInterface.ManagedInterface();

            
            double[] threeTuple1 = new double[] { 0, 0, 0 };
            double[] threeTuple2 = new double[] { 0, 0, 0 };

            //  Run the tests through the interface.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
                managedInterface.IncrementCounter();
            stopwatch.Stop();
            ManagedInterface_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                dResult += managedInterface.CalculateSquareRoot((double)i);
            }
            stopwatch.Stop();
            ManagedInterface_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                threeTuple1[0] = i;
                threeTuple1[1] = i;
                threeTuple1[2] = i;

                threeTuple2[0] = i;
                threeTuple2[1] = i;
                threeTuple2[2] = i;

                dResult += managedInterface.DotProduct(threeTuple1, threeTuple2);
            }
            stopwatch.Stop();
            ManagedInterface_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;

            //  Run the tests through the pinvoke.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
                TA_IncrementCounter();
            stopwatch.Stop();
            PInvoke_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                dResult += TA_CalculateSquareRoot((double)i);
            }
            stopwatch.Stop();
            PInvoke_Test2_Time = stopwatch.Elapsed.TotalMilliseconds;

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                threeTuple1[0] = i;
                threeTuple1[1] = i;
                threeTuple1[2] = i;

                threeTuple2[0] = i;
                threeTuple2[1] = i;
                threeTuple2[2] = i;

                dResult += TA_DotProduct(threeTuple1, threeTuple2);
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
