using System;
using System.Runtime.InteropServices;

namespace SharedData
{
    // Declares a managed structure for each unmanaged structure.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MyPerson
    {
        public string first;
        public string last;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MyPerson2
    {
        public MyPerson person;
        public int age;
    }
}
