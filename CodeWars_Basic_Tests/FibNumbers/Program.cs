using System;
using System.Collections.Generic;

namespace FibNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify prod number");
                return;
            }
            
            List<ulong> fNumbers = new List<ulong>();
            ulong prod; 

            if (args[0] == "print_fib_numbers")
            {
                fNumbers = getFibNumbers(20);
                Console.WriteLine("Fib numbers:");
                fNumbers.ForEach(Print);
            }
            else
            {
                prod = ulong.Parse(args[0]);
                object[] productFib = getProductFib(prod);
                foreach(object pr in productFib)
                    Console.WriteLine(pr);
            }   
            //List<ulong> fMultiplication = getFibMultiplication(fNumbers);
        }
        static List<ulong> getFibNumbers(int fAmount)
        {
            List<ulong> fNumbers = new List<ulong>();
            for(int i=0; i<fAmount; i++)
            {
                if((i == 0) || (i == 1))
                    fNumbers.Add(Convert.ToUInt64(i));
                else
                    fNumbers.Add(fNumbers[i-1] + fNumbers[i-2]);
            }
            return fNumbers;
        }

        static List<ulong> getFibMultiplication(List<ulong> fNumbers)
        {
            List<ulong> fMultiplication = new List<ulong>();
            for(int i=1; i<fNumbers.Count; i++)
                fMultiplication.Add(fNumbers[i]*fNumbers[i-1]);

            //Console.WriteLine("Fib multiplications:");
            //fMultiplication.ForEach(Print);
            return fMultiplication;
        }

        static object[] getProductFib(ulong prod)
        {
            int fAmount = 20;
            object[] fibDigits = new object[3];

            List<ulong> fNumbers = getFibNumbers(fAmount);
            for(int i=1; i<fNumbers.Count; i++)
            {
                ulong fMultiplication = fNumbers[i]*fNumbers[i-1];
                if (prod == fMultiplication)
                {
                    fibDigits[0] = fNumbers[i-1];
                    fibDigits[1] = fNumbers[i];
                    fibDigits[2] = true;
                }
                else if (prod > fMultiplication)
                {
                    fibDigits[0] = fNumbers[i];
                    fibDigits[1] = fNumbers[i+1];
                    fibDigits[2] = false;
                }
            }
            return fibDigits;
        }

        static void Print(ulong i)
        {
            Console.WriteLine(i);
        }
    }
}
