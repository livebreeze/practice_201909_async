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
            string url1 = $"https://lobworkshop.azurewebsites.net/api/RemoteSource/Source3";
            string url2 = $"https://lobworkshop.azurewebsites.net/api/RemoteSource/Source100";

            var sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時
            Console.WriteLine($"START!: {DateTime.Now}");

            var taskList = new List<Task>();

            var task1 = Task.Run(async () =>
            {
                await RequestUrl(url1);

            });

            var task2 = Task.Run(async () =>
            {
                await RequestUrl(url2);
            });

            taskList.Add(task1);
            taskList.Add(task2);

            try
            {
                await Task.WhenAll(taskList.ToArray());
            }
            catch (Exception ex)
            {
                var taskArr = taskList.ToArray();
                for (int i = 0; i < taskList.Count; i++)
                {
                    var currentTask = taskArr[i];
                    if (currentTask.IsFaulted)
                    {
                        Console.WriteLine($"Task {currentTask.Id} 發生錯誤: {currentTask.Exception.Message}");
                    }
                }

                //Console.WriteLine(ex);
            }

            sw.Stop();//碼錶停止

            Console.WriteLine($"請求結束，共花 {sw.Elapsed.TotalMilliseconds.ToString()} ms");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task RequestUrl(string url)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :測試開始時間 {DateTime.Now}");
            var result = await client.GetStringAsync(url);
            Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :測試結果內容 {result}");
            Console.WriteLine($" {Thread.CurrentThread.ManagedThreadId} :測試結束時間 {DateTime.Now}");
        }
    }
}
