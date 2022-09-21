using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneablePoint
{
    // Класс Point. 
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointDescription desc = new PointDescription();
        public Point(int xPos, int yPos, string petName)
        {
            X = xPos;
            Y = yPos;
            desc.PetName = petName;
        }
        public Point(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }
        public Point() { }

        // Переопределение Object.ToStringO. 
        public override string ToString()
        {
            return string.Format("X = {0}; Y = {1}; Name = {2};\nID = {3}\n", X, Y, desc.PetName, desc.PointID);
        }

        //Возврат копии текущего объекта
        public object Clone()
        {
            return new Point(this.X, this.Y);
        }
    }

    public class PointDescription
    {
        public string PetName { get; set; }
        public Guid PointID { get; set; }
        public PointDescription()
        {
            PetName = "No-name";
            PointID = Guid.NewGuid();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Cloning *****\n");
            // Две ссылки на один и тот же объект! 
            Point p1 = new Point (50, 50);
            Point p2 = p1;
            Console.WriteLine(p1);
            Console.WriteLine(p2);

            Point p3 = new Point(100, 100, "jane");
            Point p4 = (Point)p3.Clone();

            //До изменения. 
            Console.WriteLine("Before modification:");
            Console.WriteLine("p3: {0}", p3);
            Console.WriteLine("p4 : {0}", p4);

            p4.desc.PetName = "My new Point";
            p2.X = 0;
            p4.X = 9;

            Console.WriteLine("After modification:");
            Console.WriteLine(p3);
            Console.WriteLine(p4);
            Console.ReadLine();
        }
    }
}
