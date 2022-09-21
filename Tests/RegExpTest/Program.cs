using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegExpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "1 GO 2";
            string pattern = "[^a-z]";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = rgx.Replace(source,"");
            result = result.ToUpper();

            Console.WriteLine("Source: {0}", source);
            Console.WriteLine("Result: {0}", result);

            source = "this is the comment";
            pattern = "^--";
            rgx = new Regex(pattern);
            Match m = rgx.Match(source);
            Console.WriteLine("Match result: {0}", m.Success);
            if (m.Success == false)
                Console.WriteLine("Match failed");
        }
    }
}
