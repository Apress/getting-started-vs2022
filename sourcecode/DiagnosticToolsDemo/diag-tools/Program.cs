namespace diag_tools;

internal class Program
{
    static void Main(string[] args)
    {
        do
        {
            Console.WriteLine("=== Diagnostic Tools Demo ===");

            SimulateStartupDelay();

            var data = GenerateData();
            var result = ExpensiveComputation(data);

            Console.WriteLine($"Computation result: {result}");

            TriggerAnException();

            AllocateMemory();

            Console.WriteLine("Finished.");
            Console.Write("Run again? (Y to repeat, any other key to exit): ");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (char.ToUpper(key.KeyChar) != 'Y')
                break;
        }
        while (true);
    }

    static void SimulateStartupDelay()
    {
        Console.WriteLine("Simulating startup work...");
        Thread.Sleep(1000); // show event
    }

    static List<int> GenerateData()
    {
        Console.WriteLine("Generating data...");
        var rnd = new Random();
        var list = new List<int>();
        for (int i = 0; i < 1_000_000; i++)
        {
            list.Add(rnd.Next(0, 10_000));
        }
        return list;
    }

    static int ExpensiveComputation(List<int> data)
    {
        Console.WriteLine("Starting expensive computation...");

        // Artificial CPU load (Hot path)
        int result = 0;
        foreach (var number in data)
        {
            result += SlowPrimeCheck(number) ? number : 0;
        }

        return result;
    }

    static bool SlowPrimeCheck(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    static void TriggerAnException()
    {
        try
        {
            Console.WriteLine("Triggering a test exception...");
            throw new InvalidOperationException("Demo exception for Events View");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception: {ex.Message}");
        }
    }

    static void AllocateMemory()
    {
        Console.WriteLine("Allocating memory...");

        var allocations = new List<byte[]>();
        for (int i = 0; i < 100; i++)
        {
            allocations.Add(new byte[10_000_000]); // 10MB
            Thread.Sleep(50); // give time for sampling
        }

        GC.Collect();
        Console.WriteLine("Memory allocation and GC completed.");
    }

    static string DisplayMessage()
    {
        return "Hello World";
    }

}
