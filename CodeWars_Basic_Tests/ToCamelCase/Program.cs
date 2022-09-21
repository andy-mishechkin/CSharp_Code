using System;

namespace ToCamelCase
{
    class Program
    {   
        static void Main()
        {
            string SourceString = "the-stealth-warrior";
            string DestString = SourceString.Replace("-","_");
            
            char[] arrDestString = DestString.ToCharArray();
            arrDestString.SetValue(Char.ToUpper(arrDestString[0]), 0);
            for(int i=0; i < arrDestString.Length; i++) {
                if(arrDestString[i] == '_') {
                    arrDestString.SetValue(Char.ToUpper(arrDestString[i+1]), i+1);
                }
            }
            DestString = new string(arrDestString);
            Console.WriteLine(DestString);
        }
    }
}
