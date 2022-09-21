using System;

namespace MovingZerosToTheEnd
{
    class Program
    {
        public static void Main()
        {
            int[] intArr = new int[] {1, 2, 1, 1, 5, 1, 0, 3, 0, 1};
            Console.WriteLine("Source Array: {0}", string.Join(",",intArr));
            int[] retArr = MoveZeroes(intArr);
            Console.WriteLine("New Array: {0}", string.Join(",",retArr));
        }

        public static int[] MoveZeroes(int[] arr) 
        {
            int[] newarr = new int[arr.Length];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int sourceLastZeroIndex = 0;

            for(int i=0; i<arr.Length; i++)
            {
                if (arr[i] == 0) {
                    sourceLastZeroIndex = i;
                    Array.Copy(arr, sourceIndex, newarr, destinationIndex, sourceLastZeroIndex - sourceIndex);
                    destinationIndex = Array.IndexOf(newarr,0);
                    sourceIndex = sourceLastZeroIndex + 1;
                }
            }
            return newarr;
        }
    }
}
