using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming.App
{
    public class TestTasks
    {
        /*
            OUTPUT:

            ##################################################
            How does Task work?
            ##################################################
            Initializing tasks...
            Task 1 started - 23:42:39
            Task 2 started - 23:42:39
            Task 3 started - 23:42:39
            Tasks initialized...
            Task 1 is is ready - 23:42:40
            Task 2 is is ready - 23:42:41
            Task 3 is is ready - 23:42:42
            Task 1 result: 1
            Task 2 result: 2
            Task 3 result: 3
        */
        public static async Task RunAsync()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("How does Task work?");
            Console.WriteLine("##################################################");

            Console.WriteLine("Initializing tasks...");
            Task<int> task1 = Task1Async(); // The task execution starts here... yes, it's already running here.
            Task<int> task2 = Task2Async();
            Task<int> task3 = Task3Async();
            Console.WriteLine("Tasks initialized...");

            int task1Result = await task1;
            int tas2Result = await task2;
            int task3Result = await task3;

            Console.WriteLine($"Task 1 result: {task1Result}");
            Console.WriteLine($"Task 2 result: {tas2Result}");
            Console.WriteLine($"Task 3 result: {task3Result}");
        }

        private static async Task<int> Task1Async()
        {
            Console.WriteLine($"Task 1 started - {DateTime.Now:HH:mm:ss}");
            await Task.Delay(1000);
            Console.WriteLine($"Task 1 is is ready - {DateTime.Now:HH:mm:ss}");
            return 1;
        }

        private static async Task<int> Task2Async()
        {
            Console.WriteLine($"Task 2 started - {DateTime.Now:HH:mm:ss}");
            await Task.Delay(2000);
            Console.WriteLine($"Task 2 is is ready - {DateTime.Now:HH:mm:ss}");
            return 2;
        }

        private static async Task<int> Task3Async()
        {
            Console.WriteLine($"Task 3 started - {DateTime.Now:HH:mm:ss}");
            await Task.Delay(3000);
            Console.WriteLine($"Task 3 is is ready - {DateTime.Now:HH:mm:ss}");
            return 3;
        }
    }
}
