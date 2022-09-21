using System;
using System.Text.RegularExpressions;

namespace ValidParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("[!] You must specify the source bracket string");
                return;
            }
            else if (args.Length > 1)
                Console.WriteLine("[!] Error. You must specify the one argument");
            else 
            {
                string pattern = @"^(\(|\))*$";
                if(Regex.IsMatch(args[0], pattern))
                {
                    bool retValue = ValidParentheses(args[0]);
                    if(retValue)
                        Console.WriteLine("Parentheses is valid");
                    else
                        Console.WriteLine("Parentheses is not valid");
                }
                else
                    Console.WriteLine("[!] Error. String must contains brackets only");
            }       
        }
        static bool ValidParentheses(string input)
        {
            Console.WriteLine("Line is valid");

            bool retValue;
            int closedBracketCounter = 0;
            
            if(input.IndexOf(")") == 0)
                retValue = false;
            else
            {
                foreach(char bracket in input)
                {
                    if(bracket == '(')
                        closedBracketCounter++;
                    else if (bracket == ')')
                        closedBracketCounter--;
                }
                retValue = closedBracketCounter == 0 ? true : false;
            }
            return retValue;
        }
    }
}
