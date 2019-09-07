using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時
            Console.WriteLine(DateTime.Now);

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var j = string.Format("{0:D2}", i);
                string task1 = null;
                string task2 = null;

                var t1 = Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-1 測試開始時間 {DateTime.Now}");
                    task1 = await client.GetStringAsync(url);
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-1 測試結果內容 {task1}");
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-1 測試結束時間 {DateTime.Now}");
                   
                });

                var t2 = Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();
                   
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-2 測試開始時間 {DateTime.Now}");
                    task2 = await client.GetStringAsync(url);
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-2 測試結果內容 {task2}");
                    Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :第 {j}-2 測試結束時間 {DateTime.Now}");
                });
            
                tasks.Add(t1);
                tasks.Add(t2);
            }
            
            Task.WaitAll(tasks.ToArray());
            sw.Stop();//碼錶停止
            Console.WriteLine(sw.Elapsed.TotalMilliseconds.ToString());

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
