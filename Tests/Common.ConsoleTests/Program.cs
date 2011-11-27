using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Wanderer.Library.Common.ConsoleTests
{
    internal class Program
    {
        private static void Main()
        {
            DoSpeedTest();

            Console.WriteLine("Finished, press any key...");
            Console.ReadKey();
        }

        private static void DoSpeedTest()
        {
            var testObject = new BindableObjectForTest();

            testObject.PropertyChanged += (s, e) =>
                                              {
                                                  // Some dummy calculation that takes up some time
                                                  var d = 1.234d;

                                                  for (var k = 0;k < 1000;++k)
                                                  {
                                                      d = Math.Cos(d);
                                                      d = Math.Sin(d * d);
                                                      d = Math.Sqrt(d);
                                                      d = Math.Log10(d) * d;
                                                  }
                                              };
            for (var i = 0;i < 5;++i)
            {
                var iterations = 1 + i * 500;

                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseString", TimeLoop(iterations, () => { testObject.UseString = "XX"; }));
                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseStringVerified", TimeLoop(iterations, () => { testObject.UseStringVerified = "XX"; }));
                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseMethodBase", TimeLoop(iterations, () => { testObject.UseMethodBase = "XX"; }));
                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseMethodBaseVerified", TimeLoop(iterations, () => { testObject.UseMethodBaseVerified = "XX"; }));
                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseExpression", TimeLoop(iterations, () => { testObject.UseExpression = "XX"; }));
                Console.WriteLine("{0,-5} times using {1,25} => {2} ms", iterations, "UseExpressionVerified", TimeLoop(iterations, () => { testObject.UseExpressionVerified = "XX"; }));
                Console.WriteLine();
            }
        }

        private static string TimeLoop(int loopCount, Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);

            try
            {
                #region Garbage collect
                GC.Collect();
                Thread.Sleep(1);
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1);
                GC.Collect();
                Thread.Sleep(1);
                GC.WaitForPendingFinalizers();
                Thread.Sleep(1);
                GC.Collect();
                #endregion

                // Make sure that assembly loading is not a part of the measurement
                action();

                var watch = new Stopwatch();

                watch.Start();

                for (var i = 0;i < loopCount;++i)
                    action();

                watch.Stop();

                return watch.ElapsedMilliseconds.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
