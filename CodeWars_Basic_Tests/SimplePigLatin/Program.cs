using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimplePigLatin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("You must specify one source string");
                return;
            }
            else
            {
                string pigStr = PigIt(args[0]);
                Console.WriteLine(pigStr);
            }
        }

        public static string PigIt(string str)
        {
            string pigItStr = null;
            string[] words = str.Split(' ');
            var newWords = (from word in words select (changeWord(word))).ToArray<string>();
            foreach (string word in newWords)
                pigItStr += word;
            return pigItStr;
        }

        public static string changeWord(string word)
        {
            string newWord = null;
            string pattern = @"^\W*$";
            if (Regex.IsMatch(word, pattern))
                newWord = word;
            else 
            {
                char[] chWord = word.ToCharArray();
                List<char> listNewWord = new List<char>();

                for (int i = 1; i < chWord.Length; i++)
                    listNewWord.Add(chWord[i]);
                listNewWord.Add(chWord[0]);
                newWord = new string(listNewWord.ToArray<char>()) + "ay ";
            }
            return newWord;
        }
    }
}
