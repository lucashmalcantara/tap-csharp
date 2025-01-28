using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsynchronousProgramming.App
{
    public static class TestWhenAll
    {
        /*
            OUTPUT:

            ##################################################
            How does Task.WhenAll work?
            ##################################################
            Task 1 started - 23:44:22
            Task 2 started - 23:44:22
            Task 3 started - 23:44:22
            Task 1 is is ready - 23:44:23
            Task 2 is is ready - 23:44:24
            Task 3 is is ready - 23:44:25
            Results: 1,2,3
            All tasks processed.
        */
        public static async Task RunAsync()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("How does Task.WhenAll work?");
            Console.WriteLine("##################################################");

            var tasks = new List<Task<int>>
            {
                Task1Async(),
                Task2Async(),
                Task3Async()
            };

            int[] results = await Task.WhenAll(tasks);

            Console.WriteLine($"Results: {string.Join(',', results)}");

            Console.WriteLine("All tasks processed.");
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
