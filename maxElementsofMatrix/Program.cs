using maxElementsofMatrix;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();

        int threadCount = GetThreadCountFromConfig(configuration);

        Console.WriteLine(threadCount);

        int Rows = 10000; 
        int Columns = 10000;
       

        Matrix matrix = new Matrix(Rows, Columns);
        Random random = new Random();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                matrix.SetValue(i, j, random.Next(100)); 
            }
        }

        Stopwatch stopwatchParallel = new Stopwatch();
        stopwatchParallel.Start();

        int[] maxElementsParallel = matrix.FindMaxElementsThreads(threadCount);

        stopwatchParallel.Stop();
        double timeParallel = stopwatchParallel.ElapsedMilliseconds;

        // Time the execution of FindMaxElements (single-threaded)
        Stopwatch stopwatchSingleThread = new Stopwatch();
        stopwatchSingleThread.Start();

        int[] maxElementsSingleThread = matrix.FindMaxElements();

        stopwatchSingleThread.Stop();
        double timeSingleThread = stopwatchSingleThread.ElapsedMilliseconds;

        double speedup = timeSingleThread / timeParallel;

        double efficiency = timeSingleThread / (threadCount * timeParallel);

        double cost = threadCount * timeParallel;

        Console.WriteLine($"timeSingleThread (ms): {timeSingleThread}");
        Console.WriteLine($"timeParallel (ms): {timeParallel}");

        Console.WriteLine($"Speedup (Sp): {speedup}");
        Console.WriteLine($"Efficiency (Ep): {efficiency}");
        Console.WriteLine($"Cost (Cp): {cost}");
    }
    static int GetThreadCountFromConfig(IConfiguration configuration)
    {
        var threadCountConfig = configuration.GetSection("ThreadCount");

        // Check if the configuration key exists and has a valid integer value
        if (int.TryParse(threadCountConfig.Value, out int threadCount) && threadCount > 0)
        {
            return threadCount;
        }

        // If the configuration key doesn't exist or has an invalid value, use the default
        return Environment.ProcessorCount;
    }
}