using System;
using System.Collections.Generic;

namespace Permutations
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("[!] You must specify the source string");
                return;
            }
            else if (args.Length > 1)
                Console.WriteLine("[!] Error. You must specify the one argument");
            CountSymsInString(args[0]);
            List<string> Permutations = SinglePermutations(args[0]);
            foreach (string s in Permutations)
                Console.WriteLine(s);
        }

        public static void CountSymsInString(string s)
        {
            CharEnumerator chEnum = s.GetEnumerator();
            char[] sCharArr = s.ToCharArray();

            Dictionary<char, int> AmountOfSymbols = new Dictionary<char, int>();

            while (chEnum.MoveNext())
            {
                int AmountOfCurrentSym = 0;
                for (int i = 0; i < sCharArr.Length; i++)
                {
                    if (chEnum.Current == sCharArr[i])
                        AmountOfCurrentSym++;
                }
                if (!AmountOfSymbols.ContainsKey(chEnum.Current))
                    AmountOfSymbols.Add(chEnum.Current, AmountOfCurrentSym);
            }
            foreach (KeyValuePair<char, int> Amount in AmountOfSymbols)
                Console.WriteLine(Amount.Key + " - " + Amount.Value);
        }

        public static List<string> SinglePermutations(string s)
        {
            Console.WriteLine($"Source string: {s}");

            List<string> Permutations = new List<string>();
            Dictionary<string, bool> PermutadedStrings = new Dictionary<string, bool>();
            PermutadedStrings.Add(s, false);
            while (true)
            {
                bool IsComplette = false;
                Dictionary<string, bool> newPermutadedStrings = ToProcessColl(PermutadedStrings, out IsComplette);
                if (IsComplette == true)
                {
                    foreach (string pString in newPermutadedStrings.Keys)
                        Permutations.Add(pString);
                    break;
                }
                else
                {
                    PermutadedStrings.Clear();
                    foreach (string pString in newPermutadedStrings.Keys)
                        PermutadedStrings.Add(pString, newPermutadedStrings[pString]);
                }
            }
            return Permutations;
        }

        static Dictionary<string, bool> ToProcessColl(Dictionary<string, bool> strCollection, out bool IsComplette)
        {
            bool AllProcessed = false;

            Dictionary<string, bool> newStrCollection = new Dictionary<string, bool>();
            foreach (string key in strCollection.Keys)
                newStrCollection.Add(key, strCollection[key]);

            foreach (KeyValuePair<string, bool> pString in strCollection)
            {
                if (strCollection[pString.Key] == true)
                {
                    AllProcessed = true;
                    continue;
                }
                else
                {
                    AllProcessed = false;
                    char[] sCharArr = pString.Key.ToCharArray();
                    Dictionary<int, int> IndexesToSwap = new Dictionary<int, int>();
                    for (int i = 0; i < sCharArr.Length - 1; i++)
                    {
                        if (sCharArr[i] != sCharArr[i + 1])
                            IndexesToSwap.Add(i, i + 1);
                    }
                    if (IndexesToSwap.Count == 0)
                    {
                        AllProcessed = true;
                        break;
                    }

                    foreach (KeyValuePair<int, int> keyValue in IndexesToSwap)
                    {
                        SwapArrElems(keyValue.Key, keyValue.Value, sCharArr);
                        string SwappedString = new string(sCharArr);

                        if (!newStrCollection.ContainsKey(SwappedString))
                            newStrCollection.Add(SwappedString, false);

                        newStrCollection[pString.Key] = true;
                        sCharArr = pString.Key.ToCharArray();
                    }
                }
            }
            if (AllProcessed == true)
                IsComplette = true;
            else
                IsComplette = false;
            return newStrCollection;
        }

        static void SwapArrElems(int i, int j, char[] cArr)
        {
            char tmpSym = cArr[i];
            cArr[i] = cArr[j];
            cArr[j] = tmpSym;
        }
    }
}
