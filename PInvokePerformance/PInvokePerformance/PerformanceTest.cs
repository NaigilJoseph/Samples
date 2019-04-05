using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;
using SharedData;

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


        // Declares a managed prototype for unmanaged function.
        [DllImport("TraditionalAPI.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TestStructInStruct(IntPtr person2);

        [DllImport("TraditionalAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteObjectAPI(IntPtr personName);

        private double[] g_matrix1 = new double[3 * 1024 * 1024];
        private double[] g_matrix2 = new double[3 * 1024 * 1024];


        public void RunTests()
        {
            RunCliTests();
            RunPInvokeTests();

        }

        public void RunCliTests()
        {
            double dResult;

            //  Create the managed interface.
            ManagedInterface.ManagedInterface managedInterface = new ManagedInterface.ManagedInterface();

            //  Run the tests through the interface.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                managedInterface.IncrementCounter();
            }
            stopwatch.Stop();
            ManagedInterface_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            // Structure with a pointer to another structure.
            MyPerson personName = new MyPerson(){ first = "Mark", last = "Lee" };
            MyPerson2 personAll = new MyPerson2() { age = 35, person = personName };

            String res = String.Empty;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                res = managedInterface.TestStructInStruct(personAll);
            }
            stopwatch.Stop();
            ManagedInterface_Test2_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                g_matrix1[0] = i;
                g_matrix1[1] = i;
                g_matrix1[2] = i;

                g_matrix2[0] = i;
                g_matrix2[1] = i;
                g_matrix2[2] = i;

                dResult += managedInterface.DotProduct(g_matrix1, g_matrix2);
            }
            stopwatch.Stop();
            ManagedInterface_Test3_Time = stopwatch.Elapsed.TotalMilliseconds;
        }


        public void RunPInvokeTests()
        {
            double dResult;

            //  Run the tests through the pinvoke.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                TA_IncrementCounter();
            }

            stopwatch.Stop();
            PInvoke_Test1_Time = stopwatch.Elapsed.TotalMilliseconds;

            // Structure with a pointer to another structure.
            MyPerson personName = new MyPerson() { first = "Mark", last = "Lee" };
            MyPerson2 personAll = new MyPerson2() { age = 35, person = personName };

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                IntPtr buffer1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(personAll));
                Marshal.StructureToPtr(personAll, buffer1, false);

                var resultPtr = TestStructInStruct(buffer1);
                DeleteObjectAPI(resultPtr);
                Marshal.FreeCoTaskMem(buffer1);
            }
            stopwatch.Stop();
            PInvoke_Test2_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                g_matrix1[0] = i;
                g_matrix1[1] = i;
                g_matrix1[2] = i;

                g_matrix2[0] = i;
                g_matrix2[1] = i;
                g_matrix2[2] = i;

                dResult += TA_DotProduct(g_matrix1, g_matrix2);
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

        public double ManagedInterface_Test1_Time { get; set; }
        public double ManagedInterface_Test2_Time { get; set; }
        public double ManagedInterface_Test3_Time { get; set; }

        public double PInvoke_Test1_Time { get; set; }
        public double PInvoke_Test2_Time { get; set; }
        public double PInvoke_Test3_Time { get; set; }
    }
}
