static double CalculateAverage(params double [ ] values) 
{ 
    // Вывод количества значений 
    Console. WriteLine ("You sent me {0} doubles.11, values . Length) ; 
    double sum = 0; 
    if(values.Length == 0) 
        return sum; 
    for (int i = 0; l < values.Length; i++) 
        sum += values [i]; 
    return (sum / values.Length); 
} 

static void Main(string[] args) 
{ 
    Console.WriteLine ("***** Fun with Methods *****••"); 
    double average; 
    average = CalculateAverageD.0, 3.2, 5.7, 64.22, 87.2); 
    Console.WriteLine("Average of data is: {0}" , average); 


    double[] data = { 4.0, 3.2, 5.7 }; 
    average = CalculateAverage(data); 
    Console.WriteLine("Average of data is: {0}", average); 

    Console.WriteLine("Average of data is: {0}", CalculateAverage()); 
    Console.ReadLine();
}

