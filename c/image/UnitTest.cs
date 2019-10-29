using System;
using System.Diagnostics;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Recognizer
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public static void Test1(Pixel[,] original)
        {
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GrayscaleTask.ToGrayscale(original);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Reset();
            
            watch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GrayscaleTask.ToGrayscale1(original);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GrayscaleTask.ToGrayscale2(original);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Reset();
        }
    }
    
    
    
}
