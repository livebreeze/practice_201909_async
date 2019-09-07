using System;
using System.Threading;
using System.Threading.Tasks;

namespace S001.MyPractice
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"step1 {Thread.CurrentThread.ManagedThreadId}");
            await M1Async(); 
            Console.WriteLine($"step2 {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("FINISH");
            Console.ReadKey();
        }

    

        private static async Task M1Async()
        {
            Console.WriteLine($"step3 {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000);
            Console.WriteLine($"step4 {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
