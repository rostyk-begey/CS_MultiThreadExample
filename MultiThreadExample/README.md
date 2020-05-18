# C# Multi-thread example

The program consists of two functions that are running in different threads and a buffer queue that are shared between the thread

##Data buffer
__ConcurrentQueue__ has been used to illustrate data buffer where data is stored before being consumed by a consumer

Using __ConcurrentQueue__ unlike simple __Queue__ has inbuilt thread locking that helps to keep correct work with data within multiple threads  

## Consumer thread
Consumer thread function stands for take items from buffer queue and process them

___Note:__ we are using __Thread.Sleep__ to illustrate data processing_

```c#
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
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{Thread.CurrentThread.Name}]: queue is empty, waiting...");
        }
    }
}
```


##Producer thread
Producer thread stands for collect and produce data to buffer 

```c#
private static void RunProducerThread()
{
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(100);
        queue.Enqueue($"({i} from {Thread.CurrentThread.Name})");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[{Thread.CurrentThread.Name}]: produced a value: {i} | queue size {queue.Count}");
    }
}
```
