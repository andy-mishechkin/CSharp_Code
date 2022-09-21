using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineArgsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify at least one argument argument [-File] | [-Id] |");
                return;
            }
            Console.WriteLine("Arg0: {0}, type: {1} ", args[0], args[0].GetType());
            Console.WriteLine("Arg1: {0}, type: {1} ", args[1], args[1].GetType());
            Console.WriteLine("Arg2: {0}, type: {1} ", args[2], args[2].GetType());
            Console.WriteLine("Arg3: {0}, type: {1} ", args[3], args[3].GetType());
            //Console.WriteLine("Arg4: {0}, type: {1} ", args[4], args[4].GetType());

            int Arg0;
            string Arg1;

            bool test1 = int.TryParse(args[0], out Arg0);
            if(test1 == false)
                Console.WriteLine("Wrong Arg0: {0}, type: {1} ", args[0], args[0].GetType());

        }
    }
}
