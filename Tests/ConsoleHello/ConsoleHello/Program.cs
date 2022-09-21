using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHello
{
    /// <summary>
	/// Первый консольный проект - Приветствие
	/// </summary>

    public enum KernelObjects
    {
        Directory = 0x03,
        SymbolicLink = 0x04,
        Token = 0x05
    }

    class Program
    {
        /// <summary>
		/// Точка входа. Запрашивает имя и выдает приветствие
		/// </summary>
		static void Main()
        {
            int ObjType = 0x03;
            Console.WriteLine(Enum.GetName(typeof(KernelObjects), ObjType));
        }
    }
}
