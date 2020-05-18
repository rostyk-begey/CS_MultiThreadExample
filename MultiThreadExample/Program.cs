using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MultiThreadExample
{
    internal class Program
    {
        private static ConcurrentQueue<String> queue = new ConcurrentQueue<String>();
        
        public static void Main(string[] args)
        {
            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(RunProducerThread);
                thread.Name = $"Producer thread #{i}";
                thread.Start();
            }

            for (int i = 1; i <= 2; i++)
            {
                var thread = new Thread(RunConsumerThread);
                thread.Name = $"Consumer thread #{i}";
                thread.Start();
            }
        }

        private static void RunConsumerThread()
        {
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    string value;
                    queue.TryDequeue(out value);
                    if (queue.Count == 0) throw new Exception();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[{Thread.CurrentThread.Name}]: consumed a value: {value} | queue size {queue.Count}");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[{Thread.CurrentThread.Name}]: queue is empty, waiting...");
                }
            }
        }

        private static void RunProducerThread()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100 );
                queue.Enqueue($"({i} from {Thread.CurrentThread.Name})");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{Thread.CurrentThread.Name}]: produced a value: {i} | queue size {queue.Count}");
            }
        }
    }
}
