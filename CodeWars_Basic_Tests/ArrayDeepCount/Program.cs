using System;

namespace ArrayDeepCount
{
    class Program
    {
        public static void Main()
        {
            int[] intArr = new int[] {4,5,6};
            object[] objArr = new object[] {intArr, 'b','v'};
            object[] a = new object[] {1,2,3,'a',objArr};
            int r = DeepCount(a);
            
            Console.WriteLine(r);
        }
        public static int DeepCount(object a)
        {
            int totalLength = 0;
            if(a.GetType().IsArray) {
               Array arrObj = a as Array;
               foreach(var elem in arrObj) {
                   totalLength += DeepCount(elem);
               } 
            }
            else {
                totalLength++;
            }
            return totalLength;
        }
    }
}
