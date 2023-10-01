using maxElementsofMatrix;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();

        int threadCount = GetThreadCountFromConfig(configuration);

        Matrix matrix = new Matrix(3, 3);

        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[1, 0] = 4;
        matrix[1, 1] = 5;
        matrix[1, 2] = 6;
        matrix[2, 0] = 7;
        matrix[2, 1] = 8;
        matrix[2, 2] = 9;

        matrix.Print();

        int[] maxElements = matrix.FindMaxElementsThreads(threadCount);

        Console.WriteLine("Vector of Maximum Elements:");
        foreach (int maxElement in maxElements)
        {
            Console.WriteLine(maxElement);
        }
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