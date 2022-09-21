using System;

namespace DuplicateEncode
{
    class Programm
    {
        public static void Main(string[] args)
        {
            
            foreach(string arg in args)
            {
                string r = DuplicateEncode(arg);
                Console.WriteLine(r);
            }
        }
        
        public static string DuplicateEncode(string word) 
        {
            string RetWord = word.ToUpper();

            char[] wordArr = RetWord.ToCharArray();
            foreach(char symbol in wordArr)
            {
               if(RetWord.IndexOf(symbol) == RetWord.LastIndexOf(symbol))
               {
                   RetWord = RetWord.Replace(symbol, '(');
               }
               else 
               {
                   RetWord = RetWord.Replace(symbol, ')');
               }
            }
            return RetWord;
        }
    }
}
