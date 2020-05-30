namespace SuspendAndRestart
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    internal class Program
    {
        private const string ProcessName = "GTA5";
        private const int Delay = 8000;

        static void Main(string[] args)
        {
            Console.WriteLine("Suspend and restart");

            if (args.Any())
            {
                if (args.Contains("-loop"))
                {
                    LoopMode();
                }
            }

            SuspendRestart();
        }

        private static void LoopMode()
        {
            while (true)
            {
                Console.WriteLine("Press enter key to suspend {0} and resume after {1}", ProcessName, Delay);

                var enteredKey = Console.ReadKey();

                if (enteredKey.Key == ConsoleKey.Enter)
                {
                    SuspendRestart();
                }
            }
        }

        private static void SuspendRestart()
        {
            var process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (process != null)
            {
                Console.WriteLine("Suspending {0}", ProcessName);
                process.Suspend();

                Thread.Sleep(Delay);

                Console.WriteLine("Resuming {0}", ProcessName);
                process.Resume();
            }
            else
            {
                Console.WriteLine("{0} Not found", ProcessName);
            }
        }
    }
}
