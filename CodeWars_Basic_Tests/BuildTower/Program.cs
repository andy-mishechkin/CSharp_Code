using System;
//using NLog;

namespace BuildTower
{
    class Program
    {
        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify amount of floors");
                return;
            }

            int nFloors = Int32.Parse(args[0]);
            //Logger.Info("The tower will have {0} floors", nFloors);
            string[] Tower = TowerBuilder(nFloors);
            foreach(string Floor in Tower) {
                Console.WriteLine(Floor);
            }
        }
        
        static string[] TowerBuilder(int nFloors)
        {
            char Brick = '*';
            string[] Floors = new string[nFloors];
            char[] arrBricks = new char[nFloors +(nFloors -1)];
            int CenterPoint = arrBricks.Length/2;

            for(int i=0; i < nFloors; i++) {
                int pos = CenterPoint;
                Array.Fill(arrBricks,' ');
                for(pos=CenterPoint; pos >= CenterPoint-i; pos--) {;
                    arrBricks.SetValue(Brick,pos);
                }
                for (pos=CenterPoint; pos <= CenterPoint+i; pos++) {
                    arrBricks.SetValue(Brick,pos);
                }
                Floors[i] = new string(arrBricks);
            }
            return Floors;
        }
    }
}
