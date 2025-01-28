#pragma warning disable CS0162 // Unreachable code detected
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsynchronousProgramming.App
{
    public static class TestWhenAnyWithException
    {
        /*
            OUTPUT:

            ##################################################
            How does Task.WhenAny handle exceptions?
            ##################################################


            Handling exceptions appropriately:
            Task 1 started - 23:45:12
            Task 2 started - 23:45:12
            Task 3 started - 23:45:12
            System.InvalidOperationException: Task 2 threw exception.
               at AsynchronousProgramming.App.TestWhenAnyWithException.Task1Async() in D:\dev\repos\tap-csharp\AsynchronousProgramming\AsynchronousProgramming.App\TestWhenAnyWithException.cs:line 98
               at AsynchronousProgramming.App.TestWhenAnyWithException.HandleExceptionsAppropriatelyAsync() in D:\dev\repos\tap-csharp\AsynchronousProgramming\AsynchronousProgramming.App\TestWhenAnyWithException.cs:line 45


            Handling exceptions the wrong way:
            Task 1 started - 23:45:13
            Task 2 started - 23:45:13
            Task 3 started - 23:45:13
            Task 2 is is ready - 23:45:14
            System.AggregateException: One or more errors occurred. (Task 2 threw exception.)
             ---> System.InvalidOperationException: Task 2 threw exception.
               at AsynchronousProgramming.App.TestWhenAnyWithException.Task1Async() in D:\dev\repos\tap-csharp\AsynchronousProgramming\AsynchronousProgramming.App\TestWhenAnyWithException.cs:line 98
               --- End of inner exception stack trace ---
               at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)
               at AsynchronousProgramming.App.TestWhenAnyWithException.HandleExceptionsWrongWay() in D:\dev\repos\tap-csharp\AsynchronousProgramming\AsynchronousProgramming.App\TestWhenAnyWithException.cs:line 81
        */
        public static async Task RunAsync()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("How does Task.WhenAny handle exceptions?");
            Console.WriteLine("##################################################");

            await HandleExceptionsAppropriatelyAsync();
            await HandleExceptionsWrongWay();
        }

        private static async Task HandleExceptionsAppropriatelyAsync()
        {
            Console.WriteLine("\n\nHandling exceptions appropriately:");
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task HandleExceptionsWrongWay()
        {
            Console.WriteLine("\n\nHandling exceptions the wrong way:");
            try
            {
                var tasks = new List<Task<int>>
                {
                    Task1Async(),
                    Task2Async(),
                    Task3Async()
                };

                while (tasks.Count > 0)
                {
                    Task<int> finishedTask = await Task.WhenAny(tasks);

                    // When you access `finishedTask.Result` in the `HandlingExceptionsWrongWay` method, it will block the current thread until the task completes and then
                    // return the result. If the task has faulted (i.e., thrown an exception), accessing `Result` will throw an `AggregateException` that wraps the
                    // original exception (`InvalidOperationException` in this case).
                    // The code throws a `System.AggregateException` instead of an `InvalidOperationException` because accessing `finishedTask.Result` on a
                    // faulted task wraps the original exception in an `AggregateException`, whereas `await` would propagate the original exception directly.
                    Console.WriteLine($"Task result: {finishedTask.Result}");

                    tasks.Remove(finishedTask);
                }

                Console.WriteLine("All tasks processed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task<int> Task1Async()
        {
            Console.WriteLine($"Task 1 started - {DateTime.Now:HH:mm:ss}");
            await Task.Delay(1000);
            throw new InvalidOperationException("Task 2 threw exception.");
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

#pragma warning restore CS0162 // Unreachable code detected