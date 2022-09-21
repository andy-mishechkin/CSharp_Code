using System;
namespace ParamsTest
{
    class Programm
    {
        static void Main(string[] args) 
        { 
            Console.WriteLine ("***** Fun with Methods *****"); 
            double average; 
            average = CalculateAverage(4.0, 3.2, 5.7, 64.22, 87.2); 
            Console.WriteLine("Average of data is: {0}" , average);

            TstF("String", 12);

            double[] data = { 4.0, 3.2, 5.7 }; 
            average = CalculateAverage(data); 
            Console.WriteLine("Average of data is: {0}", average); 

            Console.WriteLine("Average of data is: {0}", CalculateAverage()); 
            Console.ReadLine();
        }
        static double CalculateAverage (params double [ ] values) 
        { 
            // Вывод количества значений 
            Console. WriteLine ("You sent me {0} doubles.", values.Length) ; 
            double sum = 0; 
            if(values.Length == 0) 
                return sum; 
            for (int i = 0; i < values.Length; i++) 
                sum += values [i]; 
            return (sum / values.Length); 
        }

        static void TstF(string StrValue, int IntValue)
        {
            Console.WriteLine("String value: {0}", StrValue);
            Console.WriteLine("Integer value: {0}", IntValue);
        }
    } 
}