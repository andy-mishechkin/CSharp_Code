using System;

namespace MovingZeroToTheEnd3
{
    class Program
    {
        public static void Main()
        {
            int[] arr = new int[] {1, 0, 8, 9, 3, 0, 6, 1, 0, 4, 1};
            int[] ZeroItems = Array.FindAll(arr, arrElement => arrElement == 0);
            int[] ZeroIndexes = new int[ZeroItems.Length];
            Console.WriteLine("Source array: {0}", string.Join(",",arr));

            for(int i = 0; i < arr.Length; i++)
            {
                int j=0;
                if (arr[i] == 0) { 
                    ZeroIndexes.SetValue(i,j);
                    j++;
                }
            }
            Console.WriteLine("Final array: {0}", string.Join(",",arr));
        }
    }
}
