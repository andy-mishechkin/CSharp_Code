using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Test6
{
    class Program
    {
        static void Main()
        {

            int n = 5; string c3 = new string('a', 2 * n);
            //string c1 = new string();
            char[] car = new char[3]; car[1] = 'a'; car[2] = 'b'; string s = new string(car, 0, 2);
            //string c2 = new string("ABC");

            string ss = "string";
            char ch = 'a';
            //ss[1] = ch;

            string X1 = "\\c\x58";
            string X2 = @"\cX";
            if(X1 == X2)
            {
                Console.WriteLine("EQUALS!!!");
            }
        }

        static string FindMatch(string str, string strpat)
        {
            Regex pat = new Regex(strpat);
            Match match = pat.Match(str);
            string found = "";
            if (match.Success)
            {
                found = match.Value;
                Console.WriteLine("Строка ={0} tОбразец={1} Найдено ={2}", str,strpat,found);
            }
            return (found);
        }

        public void TestSinglePat()
        {
            //поиск по образцу первого вхождения
            string str, strpat, found;
            Console.WriteLine("Поиск по образцу");
            //образец задает подстроку, начинающуюся с символа a,
            //далее идут буквы или цифры.
            str = "start"; strpat = @"a\w+";
            found = FindMatch(str, strpat);
            str = "fab77cd efg";
            found = FindMatch(str, strpat);
            //образец задает подстроку,начинающуюся с символа a,
            //заканчивающуюся f с возможными символами b и d в середине
            strpat = "a(b|d)*f"; str = "fabadddbdf";
            found = FindMatch(str, strpat);
            //диапазоны и escape-символы
            strpat = "[X-Z]+"; str = "aXYb";
            found = FindMatch(str, strpat);
            strpat = @"\u0058Y\x5A"; str = "aXYZb";
            found = FindMatch(str, strpat);
        }//TestSinglePa
    }
}
