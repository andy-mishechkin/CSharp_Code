using System;

namespace MovingZeroToTheEnd2
{
    class Program
    {
        public static void Main()
        {
            int[] arr = new int[] {1, 0, 8, 9, 3, 0, 6, 1, 0, 4, 1};
            int[] ZeroItems = Array.FindAll(arr, arrElement => arrElement == 0);
            Console.WriteLine("Source array: {0}", string.Join(",",arr));

            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == 0) 
                {
                    while((i < (arr.Length - ZeroItems.Length) && (arr[i] == 0)))
                    {
                        for(int j=i; j < arr.Length - 1; j++)
                        {
                            arr.SetValue(arr[j+1], j);
                        }
                        arr.SetValue(0, arr.Length - 1);
                    }
                }
            }
            Console.WriteLine("Final array: {0}", string.Join(",",arr));
        }
    }       
}