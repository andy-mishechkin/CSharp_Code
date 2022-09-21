using System;

namespace SumOfDigits
{
    class Program
    {
        static void Main(string [] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify source word");
                return;
            }
            int r = DigitRoot(System.Int64.Parse(args[0]));
            Console.WriteLine("Return: {0}", r);
        }
        public static int DigitRoot(long n)
        {
            int DigitsSum = 0;
            char[] charDigits = n.ToString().ToCharArray();
            while(charDigits.Length > 1) {
                DigitsSum = 0;
                for(int i=0; i<charDigits.Length; i++) 
                {
                    DigitsSum += (int)Char.GetNumericValue(charDigits[i]);
                }
                charDigits = DigitsSum.ToString().ToCharArray();
            }
            return DigitsSum;
        }
    }
}
