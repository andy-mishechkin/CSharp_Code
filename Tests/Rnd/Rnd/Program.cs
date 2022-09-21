using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rnd
{
    class Program
    {
        static void Main()
        {
            int ProcessId = 15616;
			try
			{
                Process tProcess = Process.GetProcessById(ProcessId);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("[!]Error: {0}", e.Message);
                Environment.Exit(0);
            }
			Console.WriteLine("END!");
        }
    }
}
