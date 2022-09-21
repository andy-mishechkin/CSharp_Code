using System;
using System.Reflection;

namespace Types
{
    /// <summary>
    /// Проект Types содержит примеры, иллюстрирующие работу
    /// со встроенными скалярными типами языка С#.
    /// Проект содержит классы: Testing, Class1.
    /// 
    /// </summary>
    class Class1
    {
        /// <summary>
        /// Точка входа проекта.
        /// В ней создается объект класса Testing
        /// и вызываются его методы.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Testing tm = new Testing();
            Console.WriteLine("Testing.Who Test");
            tm.WhoTest();
            Console.WriteLine("Testing.Back Test");
            tm.BackTest();
            Console.WriteLine("Testing.OLoad Test");
            tm.OLoadTest();
            /*
            Console.WriteLine("Testing.ToString Test");
            tm.ToStringTest();
            Console.WriteLine("Testing.FromString Test");
            tm.FromStringTest();
            */
            Console.WriteLine("Testing.CheckUncheck Test");
            tm.CheckUncheckTest();
        }
    }

    /// <summary>
	/// Класс Testing включает данные разных типов. Каждый его 
	/// открытый метод описывает некоторый пример, 
	/// демонстрирующий работу с типами.
	/// Открытые методы могут вызывать закрытые методы класса.
	/// </summary>
	public class Testing
    {
        /// <summary>
        /// набор скалярных данных разного типа.
        /// </summary>
        byte b = 255;
        int x = 11;
        uint ux = 1111;
        float y = 5.5f;
        double dy = 5.55;
        string s = "Hello!";
        string s1 = "25";
        object obj = new Object();

        string name; //имя точки

        // Далее идут методы класса, приводимые по ходу 
        // описания примеров

        public void WhoTest()
        {
            WhoIsWho("x", x);
            WhoIsWho("ux", ux);
            WhoIsWho("y", y);
            WhoIsWho("dy", dy);
            WhoIsWho("s", s);
            WhoIsWho("11 + 5.55 + 5.5f", 11 + 5.55 + 5.5f);
            obj = 11 + 5.55 + 5.5f;
            WhoIsWho("obj", obj);
        }

        /// <summary>
        /// Метод выводит на консоль информацию о типе и 
        /// значении фактического аргумента. Формальный 
        /// аргумент имеет тип object. Фактический аргумент 
        /// может иметь любой тип, поскольку всегда 
        /// допустимо неявное преобразование в тип object.
        /// </summary>
        /// <param name="name"> - Имя второго аргумента</param>
        /// <param name="any"> - Допустим аргумент любого типа</param>
        void WhoIsWho(string name, object any)
        {
            Type t = any.GetType();
            Console.WriteLine("Тип {0}: {1} , значение: {2}", name, any.GetType(), any.ToString());
            Console.WriteLine("Методы класса:");
            MethodInfo[] ClassMethods = t.GetMethods();
            foreach (MethodInfo curMethod in ClassMethods)
            {
                Console.WriteLine(curMethod);
            }
            Console.WriteLine("Все члены класса:");
            MemberInfo[] ClassMembers = t.GetMembers();
            foreach (MemberInfo curMember in ClassMembers)
            {
                Console.WriteLine(curMember.ToString());
            }
        }
        /// <summary>
        /// Возвращает переданный ему аргумент.
        /// Фактический аргумент может иметь произвольный тип.
        /// Возвращается всегда объект класса object.
        /// Клиент, вызывающий метод, должен при необходимости 
        /// задать явное преобразование получаемого результата
        /// </summary>
        /// <param name="any"> Допустим любой аргумент</param>
        /// <returns></returns>
        object Back(object any)
        {
            return (any);
        }
        /// <summary>
        /// Неявное преобразование аргумента в тип object
        /// Явное приведение типа результата.
        /// </summary>
        public void BackTest()
        {
            ux = (uint)Back(ux);
            WhoIsWho("ux", ux);
            s1 = (string)Back(s);
            WhoIsWho("s1", s1);
            x = (int)(uint)Back(ux);
            WhoIsWho("x", x);
            y = (float)(double)Back(11 + 5.55 + 5.5f);
            WhoIsWho("y", y);
        }
        /// <summary>
        /// Группа перегруженных методов OLoad
        /// с одним или двумя аргументами арифметического типа.
        /// Если фактический аргумент один, то будет вызван один из 
        /// методов, наиболее близко подходящий по типу аргумента.
        /// При вызове метода с двумя аргументами возможен 
        /// конфликт выбора подходящего метода, приводящий 
        /// к ошибке периода компиляции.
        /// </summary>
        void OLoad(float par)
        {
            Console.WriteLine("float value {0}", par);
        }
        /// <summary>
        /// Перегруженный метод OLoad с одним параметром типа long
        /// </summary>
        /// <param name="par"></param>
        void OLoad(long par)
        {
            Console.WriteLine("long value {0}", par);
        }
        /// <summary>
        /// Перегруженный метод OLoad с одним параметром типа ulong
        /// </summary>
        /// <param name="par"></param>
        void OLoad(ulong par)
        {
            Console.WriteLine("ulong value {0}", par);
        }
        /// <summary>
        /// Перегруженный метод OLoad с одним параметром типа double
        /// </summary>
        /// <param name="par"></param>
        void OLoad(double par)
        {
            Console.WriteLine("double value {0}", par);
        }
        /// <summary>
        /// Перегруженный метод OLoad с двумя параметрами типа long и long
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        void OLoad(long par1, long par2)
        {
            Console.WriteLine("long par1 {0}, long par2 {1}", par1, par2);
        }
        /// <summary>
        /// Перегруженный метод OLoad с двумя параметрами типа 
        /// double и double
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        void OLoad(double par1, double par2)
        {
            Console.WriteLine("double par1 {0}, double par2 {1}", par1, par2);
        }
        /// <summary>
        /// Перегруженный метод OLoad с двумя параметрами типа 
        /// int и float
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        void OLoad(int par1, float par2)
        {
            Console.WriteLine("int par1 {0}, float par2 {1}", par1, par2);
        }
        /// <summary>
        /// Вызов перегруженного метода OLoad. В зависимости от 
        /// типа и числа аргументов вызывается один из методов группы.
        /// </summary>
        public void OLoadTest()
        {
            Console.WriteLine("Int type to Oload");
            OLoad(x);
            Console.WriteLine("UInt type to Oload");
            OLoad(ux);
            Console.WriteLine("Float type to Oload");
            OLoad(y);
            Console.WriteLine("Double type to Oload");
            OLoad(dy);
            // OLoad(x,ux); 
            // conflict: (int, float) и (long,long)
            Console.WriteLine("Int and  uint->float types to Oload");
            OLoad(x, (float)ux);
            Console.WriteLine("Float and double types to Oload");
            OLoad(y, dy);
            Console.WriteLine("Int and double type to Oload");
            OLoad(x, dy);
        }
        /// <summary>
        /// Демонстрация проверяемых и непроверяемых преобразований.
        /// Опасные проверяемые преобразования приводят к исключениям. 
        /// Опасные непроверяемые преобразования приводят к неверным 
        /// результатам, что совсем плохо.
        /// </summary>
        public void CheckUncheckTest()
        {
            x = -25 ^ 2;
            WhoIsWho("x", x);
            b = 255;
            WhoIsWho("b", b);
            // Проверяемые опасные преобразования.
            // Возникают исключения, перехватываемые catch-блоком.
            checked
            {
                try
                {
                    b += 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Переполнение при вычислении b");
                    Console.WriteLine(e);
                }
                try
                {
                    b = (byte)x;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Переполнение при преобразовании к byte");
                    Console.WriteLine(e);
                }
                // непроверяемые опасные преобразования
                unchecked
                {
                    try
                    {
                        b += 1;
                        WhoIsWho("b", b);
                        b = (byte)x;
                        WhoIsWho("b", b);
                        ux = (uint)x;
                        WhoIsWho("ux", ux);
                        Console.WriteLine("Исключений нет, но результаты не верны!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Этот текст не должен появляться");
                        Console.WriteLine(e);
                    }
                    // автоматическая проверка преобразований в Convert
                    // исключения возникают, несмотря на unchecked
                    try
                    {
                        b = Convert.ToByte(x);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Переполнение при преобразовании к byte!");
        
                        Console.WriteLine(e);
                    }
                    try
                    {
                        ux = Convert.ToUInt32(x);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Потеря знака при преобразовании к uint!");
        
                        Console.WriteLine(e);
                    }
                }
            }
        }
        /// <summary>
        /// Анализ области видимости переменных
        /// </summary>
        /// <param name="x"></param>
        public void ScopeVar(int x)
        {
            int y = 77;
            string s = name;
            if (s == "Точка1")
            {
                int u = 5; int v = u + y; x += 1;
                Console.WriteLine("y= {0}; u={1}; ={ 2}; x ={ 3}", y,u,v,x);
            }
            else
            {
                int u = 7; int v = u + y;
                Console.WriteLine("y= {0}; u={1}; v={2}", y, u, v);
            }
            //Console.WriteLine("y= {0}; u={1}; v={2}",y,u,v);
            //Локальные переменные не могут быть статическими.
            //static int Count = 1;
            //Ошибка: использование sum до объявления
            //Console.WriteLine("x= {0}; sum ={1}", x,sum);
            int i; long sum = 0;
            for (i = 1; i < x; i++)
            {
                //ошибка: коллизия имен: y
                //float y = 7.7f;
                sum += i;
            }
            Console.WriteLine("x= {0}; sum ={1}", x, sum);
        }//ScopeVar

        /// <summary>
        /// определение размеров и типов
        /// </summary>
        unsafe public static void SizeMethod()
        {
            Console.WriteLine("Размер типа Boolean = " + sizeof(bool));
            Console.WriteLine("Размер типа double = " + sizeof(double));
            Console.WriteLine("Размер типа char = " + sizeof(System.Char));
            int b1 = 1;
            Type t = b1.GetType();
            Console.WriteLine("Тип переменной b1: {0}", t);
            //Console.WriteLine("Размер переменной b1: {0}", sizeof(t));
        }//SizeMethod

         /// <summary>
         /// Арифметические операции
         /// </summary>
        public void Ariphmetica()
        {
            int n = 7, m = 3, p, q;
            p = n / m; q = p * m + n % m;
            if (q == n)
                Console.WriteLine("q=n");
            else
                Console.WriteLine("q!=n");
            double x = 7, y = 3, u, v, w;
            u = x / y; v = u * y;
            w = x % y;
            if (v == x)
                Console.WriteLine("v=x");
            else
                Console.WriteLine("v!=x");
            decimal d1 = 7, d2 = 3, d3, d4, d5;
            d3 = d1 / d2; d4 = d3 * d2;
            d5 = d1 % d2;
            if (d4 == d1)
                Console.WriteLine("d4=d1");
            else
                Console.WriteLine("d4!=d1");
        }

    }
}
