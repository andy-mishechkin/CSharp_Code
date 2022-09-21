using System;

namespace FindTheMissingLeter
{
    class Program
    {
        private static string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify source word");
                return;
            }
            foreach(string arg in args)
            {
                string strToLower = arg.ToLower();
                if (Alphabet.Contains(strToLower)) 
                {
                    Console.Write("Array {0} have no missing symbols", arg);   
                }
                else
                {
                    char r = FindMissingLetter(strToLower.ToCharArray());
                    if(r != ' ') {
                         Console.WriteLine("Missing Symbol: {0}", r);
                    }
                }
            }
        }

        public static char FindMissingLetter(char[] array)
        {
            char MissingSymbol = ' ';
            string arrString = new string(array);            
            char[] arrAlphabet = Alphabet.ToCharArray();
            int FirstIndex = Array.IndexOf(arrAlphabet, array[0]);
            int LastIndex = Array.IndexOf(arrAlphabet, array[array.Length - 1]);
            if ((LastIndex - FirstIndex + 1) > array.Length) 
            {
                for(int i = FirstIndex ; i <= LastIndex; i++)
                {
                    if(!(arrString.Contains(arrAlphabet[i])))
                    {
                        MissingSymbol = arrAlphabet[i];
                    }
                }
            } 
            return MissingSymbol;
        }
    }
}
