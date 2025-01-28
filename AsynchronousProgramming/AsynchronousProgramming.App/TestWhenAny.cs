using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsynchronousProgramming.App
{
    public static class TestWhenAny
    {
        /*
            OUTPUT:

            ##################################################
            How does Task.WhenAny work?
            ##################################################
            Task 1 started - 23:44:46
            Task 2 started - 23:44:46
            Task 3 started - 23:44:46
            Task 1 is is ready - 23:44:47
            Task result: 1
            Task 2 is is ready - 23:44:48
            Task result: 2
            Task 3 is is ready - 23:44:49
            Task result: 3
            All tasks processed.
        */

        public static async Task RunAsync()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("How does Task.WhenAny work?");
            Console.WriteLine("##################################################");

            var tasks = new List<Task<int>>
            {
                Task1Async(),
                Task2Async(),
                Task3Async()
            };

            while (tasks.Count > 0)
            {
                Task<int> finishedTask = await Task.WhenAny(tasks);

                // This ensures the program properly handles the completion of the finished task. If the task threw an exception, this line ensures the exception is
                // re-thrown so it can be handled appropriately.
                // The line `await Task.WhenAny` doesn't await the finished task. It awaits the `Task` returned by `Task.WhenAny`. The result of `Task.WhenAny` is the
                // task that has completed (or faulted). You should `await` that task again, even though you know it's finished running. That's how you retrieve its result, or
                // ensure that the exception causing it to fault gets thrown.
                var result = await finishedTask;

                Console.WriteLine($"Task result: {result}");

                tasks.Remove(finishedTask);
            }

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
