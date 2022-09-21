using System;
using System.Collections.Generic;
using System.Linq;

namespace SumOfIntervals
{
    class Program
    {
        static void Main()
        {
            int[][] intervals = new int[3][];
            
            intervals[0] = new int[2] { 1, 2 };
            intervals[1] = new int[2] { 6, 10 };
            intervals[2] = new int[2] { 11, 15 };
            
            /*
            intervals[0] = new int[2] { 1, 4 };
            intervals[1] = new int[2] { 7, 10 };
            intervals[2] = new int[2] { 3, 5 };
          
            int[][] intervals = new int[5][];

            intervals[0] = new int[2] { 1, 5 };
            intervals[1] = new int[2] { 10, 20 };
            intervals[2] = new int[2] { 1, 6 };
            intervals[3] = new int[2] { 16, 19 };
            intervals[4] = new int[2] { 5, 11 };
            */

            List<List<int>> fullIntervals = new List<List<int>>();

            foreach(int[] interval in intervals)
            {
                List<int> fullInterval = Enumerable.Range(interval[0], (interval[1] - interval[0] + 1)).ToList<int>();
                Console.WriteLine($"Full interval: {fullInterval[0]} - {fullInterval[fullInterval.Count - 1]}");
                fullInterval.ForEach(Print);
                fullIntervals.Add(fullInterval);
            }

            while(true)
            {
                List<List<int>> rIntervals = CheckIntersection(fullIntervals);
                
                int fiCount = fullIntervals.Count;
                int riCount = rIntervals.Count;
                
                fullIntervals.Clear();
                fullIntervals.InsertRange(0,rIntervals);

                if(fiCount == riCount)
                    break;
            }      

            Console.WriteLine("Print of final intervals");
            int SumOfIntervals = 0;
            foreach(List<int> finalInterval in fullIntervals)
            {
                Console.WriteLine($"Final interval: {finalInterval[0]} - {finalInterval[finalInterval.Count - 1]}");
                finalInterval.ForEach(Print);
                SumOfIntervals += finalInterval.Max() - finalInterval.Min();
            }
            Console.WriteLine($"Sum of intervals: {SumOfIntervals}");
        }

        static  List<List<int>> CheckIntersection (List<List<int>> intervals)
        {
            List<List<int>> rIntervals = new List<List<int>>();
            int numOfIntersect = 0;

            for(int k=1; k<intervals.Count; k++)
            {
                var result = intervals[0].Intersect(intervals[k]);
                Console.WriteLine($"Result of intersection: {result.Count()} elements");
                if(result.Count() > 0)
                {
                    var rInterval = intervals[0].Union(intervals[k]);
                    rIntervals.Add(rInterval.ToList<int>());
                    numOfIntersect++;
                }
                else
                    rIntervals.Add(intervals[k].ToList<int>());
            }
            if(numOfIntersect ==0)
                rIntervals.Add(intervals[0]);

            return rIntervals;
        }

        static void Print(int i)
        {
            Console.WriteLine(i);
        }
    }
}
