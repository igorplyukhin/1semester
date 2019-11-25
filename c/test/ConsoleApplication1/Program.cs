using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Gay : Object
    {
        public static string Name;
    }

    public class NotGay
    {
        public static string Name;
    }

    public class Program
    {
        public static void SetName(object gay)
        {
            gay.Name = "Andrew";
            Console.WriteLine(Gay.Name);
        }

        public static void Main()
        {
            var gay = new Gay();
            SetName(gay);
        }
        
    }
}