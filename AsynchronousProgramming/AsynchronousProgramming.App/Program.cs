using System.Threading.Tasks;

namespace AsynchronousProgramming.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await TestTasks.RunAsync();
            await TestWhenAll.RunAsync();
            await TestWhenAny.RunAsync();
            await TestWhenAnyWithException.RunAsync();
        }
    }
}