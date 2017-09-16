using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ThresholdTester
{
    class Program
    {
        static void PrintUsage()
        {
            Console.WriteLine("Usage: {0} <ErrorThreshold> <Program1> <Program2>...", AppDomain.CurrentDomain.FriendlyName);
        }

        static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintUsage();
                return -1;
            }

            int threshold = 0;
            try
            {
                threshold = int.Parse(args[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("<ErrorThreshold> needs to be a number");
                PrintUsage();
                return -1;
            }
            
            int errorCount = 0;
            for( int i = 1; i < args.Length; i++ )
            {
                int exitCode = 0;
                try
                {
                    Process p = Process.Start(args[i]);
                    p.WaitForExit();
                    exitCode = p.ExitCode;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    exitCode = -1;
                }

                if (exitCode != 0)
                {
                    errorCount++;
                    if (errorCount >= threshold)
                        return errorCount;
                }
            }

            return 0;
        }
    }
}
