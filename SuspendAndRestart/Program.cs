namespace SuspendAndRestart
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    internal class Program
    {
        private static string processName;
        private static int delay = 8000;

        static void Main(string[] args)
        {
            // ProcessName as in EXE name without extension
            // SuspendDelay as integer miliseconds value

            //Expected command args <ProcessName> <SuspendDelay>
            // E.g. Notepad 10000

            Console.WriteLine("Suspend and restart");

            if (args.Any())
            {
                if (args.Contains("-loop"))
                {
                    LoopMode();
                }
            }

            if(args.Length >= 1)
            {
                processName = args[0].Trim();
            }

            if (args.Length >= 2)
            {
                delay = int.Parse(args[1].Trim());
            }

            SuspendRestart();
        }

        private static void LoopMode()
        {
            while (true)
            {
                Console.WriteLine("Press enter key to suspend {0} and resume after {1}", processName, delay);

                var enteredKey = Console.ReadKey();

                if (enteredKey.Key == ConsoleKey.Enter)
                {
                    SuspendRestart();
                }
            }
        }

        private static void SuspendRestart()
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process != null)
            {
                Console.WriteLine("Suspending {0}", processName);
                process.Suspend();

                Thread.Sleep(delay);

                Console.WriteLine("Resuming {0}", processName);
                process.Resume();
            }
            else
            {
                Console.WriteLine("{0} Not found", processName);
            }
        }
    }
}
