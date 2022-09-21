using System;
using System.Collections;

namespace IssueWithNogenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleBoxUnboxOperation();
            Console.ReadLine();
        }

        static void SimpleBoxUnboxOperation()
        {
            // Создать переменную ValueType (int). 
            int mylnt = 25;
            //Boxing
            object boxedInt = mylnt;
            //Unboxing
            int unboxedInt = (int)boxedInt;
            try
            {
                long unboxedlnt = (long)boxedInt;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
