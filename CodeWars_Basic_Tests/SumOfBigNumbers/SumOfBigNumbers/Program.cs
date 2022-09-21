using System;
using System.Collections.Generic;
using System.Linq;

namespace SumOfBigNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("You must specify two arguments");
                return;
            }
            string sRes = SumOfBigDigits(args[0], args[1]);
            if(sRes != null)
                Console.WriteLine($"Result: {sRes}");
        }

        static string SumOfLongDigits(string N1, string N2)
        {
            string sRes = null;
            try
            {
                long lN1 = Convert.ToInt64(N1);
                long lN2 = Convert.ToInt64(N2);
                sRes = Convert.ToString(lN1 + lN2);
            }
            catch
            {
                Console.WriteLine("Input strings have a wrong format");
            }
            return sRes;
        }

        static string SumOfBigDigits(string N1, string N2)
        {
            string sRes = null;

            if (N1.Length > N2.Length)
                N2 = N2.Insert(0, new string('0', N1.Length - N2.Length));
            else if (N2.Length > N1.Length)
                N1 = N1.Insert(0, new string('0', N2.Length - N1.Length));

            char[] chN1 = N1.ToCharArray();
            char[] chN2 = N2.ToCharArray();
            Stack<int> stRes = new Stack<int>();

            int elemSumRes;
            List<int> stIncrements = new List<int>();
            for(int i = chN1.Length - 1; i>=0; i--)
            {
                try 
                {
                    int elemSum = (int)Char.GetNumericValue(chN1[i]) + (int)Char.GetNumericValue(chN2[i]);
                    if ((elemSum > 9) && (i != 0))
                    {
                        var strDigits = (from digit in (elemSum.ToString()) select digit).ToArray<char>();
                        elemSumRes = (int)Char.GetNumericValue(strDigits[1]);
                        stIncrements.Add(i - 1);
                    }
                    else
                        elemSumRes = elemSum;
                    if (stIncrements.Contains(i))
                        elemSumRes++;
                    stRes.Push(elemSumRes);
                }
                catch
                {
                    Console.WriteLine("Input strings have a wrong format");
                    return sRes;
                }
            }
            int[] arrRes = stRes.ToArray();

            foreach (int num in arrRes)
                sRes += num.ToString();
            return sRes;
        }
    }
}
