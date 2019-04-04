using SharedData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace pInvokePerformanceCore
{
    public class PerformanceTestCore
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

        // Declares a managed prototype for unmanaged function.
        [DllImport("TraditionalAPI.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TestStructInStructAPI(IntPtr person2);

        [DllImport("TraditionalAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteStringAPI(IntPtr personName);

        
        public void RunTests()
        {
            double dResult = 0;
         
            double[] matrix1 = new double[3 * 1024 *1024];
            double[] matrix2 = new double[3 * 1024 * 1024];

            //  Run the tests through the pinvoke.
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
                TA_IncrementCounter();
            stopwatch.Stop();
            PInvoke_Test1_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);

            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                // Structure with a pointer to another structure.
                MyPerson personName;
                personName.first = "Mark";
                personName.last = "Lee";

                MyPerson2 personAll;
                personAll.age = 30;

                personAll.person = personName;

                IntPtr buffer1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(personAll));
                Marshal.StructureToPtr(personAll, buffer1, false);

                var res = PerformanceTestCore.TestStructInStructAPI(buffer1);
                DeleteStringAPI(res);
                Marshal.FreeCoTaskMem(buffer1);
            }
            stopwatch.Stop();
            PInvoke_Test2_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);

            //dResult = 0;
            //stopwatch.Restart();
            //for (ulong i = 1; i <= testCount; i++)
            //{
            //    dResult += TA_CalculateSquareRoot((double)i);
            //}
            //stopwatch.Stop();
            //PInvoke_Test2_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);

            dResult = 0;
            stopwatch.Restart();
            for (ulong i = 1; i <= testCount; i++)
            {
                matrix1[0] = i;
                matrix1[1] = i;
                matrix1[2] = i;

                matrix2[0] = i;
                matrix2[1] = i;
                matrix2[2] = i;

                dResult = TA_DotProduct(matrix1, matrix2);
            }
            stopwatch.Stop();
            PInvoke_Test3_Time = Math.Round(stopwatch.Elapsed.TotalMilliseconds, 4);
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
