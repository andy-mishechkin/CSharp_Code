using System;
using System.Collections.Generic;

namespace Snail
{
    class Program
    {
        delegate bool CheckFinalRing(int StartPos, int EndPos);
        static void Main(string[] args)
        {
            int[][] sArr1 = new int[3][];
            sArr1[0] = new int[3] {1,2,3};
            sArr1[1] = new int[3] {4,5,6};
            sArr1[2] = new int[3] {7,8,9};

            int[][] sArr2 = new int[4][];
            sArr2[0] = new int[4] {1,2,3,1};
            sArr2[1] = new int[4] {4,5,6,4};
            sArr2[2] = new int[4] {7,8,9,7};
            sArr2[3] = new int[4] {7,8,9,7};

            int[][] sArr3 = new int[5][];
            sArr3[0] = new int[5] {1,2,3,1,2};
            sArr3[1] = new int[5] {4,5,6,4,4};
            sArr3[2] = new int[5] {7,8,9,7,7};
            sArr3[3] = new int[5] {7,8,9,7,7};
            sArr3[4] = new int[5] {7,8,9,7,7};

            List<List<int>> Rings = GetRings(sArr1);
            for(int i=0; i<Rings.Count; i++)
            {
                Console.WriteLine("Ring {0}", i);
                foreach (int r in Rings[i])
                    Console.WriteLine(r);
            }

            int[] iSnail = Snail(sArr1);
            Console.WriteLine("Snail:");
            for (int i = 0; i < iSnail.Length; i++)
                Console.WriteLine(iSnail[i]);
        }

        static List<List<int>> GetRings(int[][] sArr)
        {
            int StartPos = 0;
            int EndPos = sArr[StartPos].Length - 1;

            List<List<int>> Rings = new List<List<int>>();
            CheckFinalRing checkFinRing;
            if (sArr[0].Length % 2 == 0)
                checkFinRing = DiffChecking;
            else
                checkFinRing = EqChecking;

            bool allRingsBuild = false;
            do
            {                
                List<int> Ring = new List<int>();

                bool finRing = checkFinRing(StartPos, EndPos);
                bool SkipRingBuilding = false;
                if (finRing)
                {
                    allRingsBuild = true;
                    if (checkFinRing == EqChecking)
                    {
                        SkipRingBuilding = true;
                        Ring.Add(sArr[StartPos][StartPos]);
                    }
                }

                if (!SkipRingBuilding)
                {
                    Ring.AddRange((MoveRight(StartPos, EndPos, sArr[StartPos])).ToArray());
                    Ring.AddRange((MoveDown(StartPos, EndPos, EndPos, sArr)).ToArray());
                    Ring.AddRange((MoveLeft(EndPos, StartPos, sArr[EndPos])).ToArray());
                    Ring.AddRange((MoveUp(EndPos, StartPos, StartPos, sArr)).ToArray());
                }
                Rings.Add(Ring);
                StartPos++;
                EndPos--;
            } while (!allRingsBuild);
            return Rings;
        }

        private static bool DiffChecking(int StartPos, int EndPos)
        {
            return (EndPos - StartPos == 1 ? true : false);
        }

        private static bool EqChecking(int StartPos, int EndPos)
        {
            return (EndPos == StartPos ? true : false);
        }

        static int[] Snail(int[][] sArr)
        {
            List<int> lSnail = new List<int>();
            List<List<int>> Rings = GetRings(sArr);
            foreach(List<int> Ring in Rings)
            {
                foreach (int r in Ring)
                    lSnail.Add(r);
            }
            return lSnail.ToArray();
        }

        static List<int> MoveRight(int StartPos, int EndPos, int[] sArr)
        {
            List<int> iSide = new List<int>();
            for (int i=StartPos; i<EndPos; i++)
                iSide.Add(sArr[i]);
            return iSide;
        }

        static List<int> MoveDown(int StartPos, int EndPos, int Column, int[][] sArr)
        {
            List<int> iSide = new List<int>();
            for (int i=StartPos; i<EndPos; i++)
                iSide.Add(sArr[i][Column]);
            return iSide;
        }

        static List<int> MoveLeft(int StartPos, int EndPos, int[] sArr)
        {
            List<int> iSide = new List<int>(); 
            for(int i=StartPos; i>EndPos; i--)
                 iSide.Add(sArr[i]);
            return iSide;
        }

        static List<int> MoveUp(int StartPos, int EndPos, int Column, int[][] sArr)
        {
            List<int> iSide = new List<int>();
            for (int i=StartPos; i>EndPos; i--)
                iSide.Add(sArr[i][Column]);
            return iSide;
        }
    }
}
