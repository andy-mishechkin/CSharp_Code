using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** A First Look at Interfaces *****\n");
            // Все эти классы поддерживают интерфейс ICloneable. 
            string myStr = "Hello";
            OperatingSystem unixOS = new OperatingSystem(PlatformID.Unix, new Version());
            System.Data.SqlClient.SqlConnection sqlCnn = new System.Data.SqlClient.SqlConnection();
            // Поэтому все они могут передаваться методу, 
            // принимающему ICloneable в качестве параметра. 
            CloneMe(myStr);
            CloneMe(unixOS);
            CloneMe(sqlCnn);
            Console.ReadLine();
        }
        private static void CloneMe(ICloneable c)
        {
            // Клонируем любой получаемый тип 
            //и выводим его имя в окне консоли. 
            object theClone = c.Clone();
            Console.WriteLine("Your clone is a: {0}", theClone.GetType().Name);
        }
    }

}