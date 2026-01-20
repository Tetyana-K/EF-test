using System;
using System.IO;
using System.Threading;

class Program
{
    static int[] numbers = new int[10000];
    static int min, max;
    static double avg;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Вивести числа 0..50");
            Console.WriteLine("2. Ввести межi дiапазону i вивести числа");
            Console.WriteLine("3. Ввести межi дiапазону та кiлькiсть потокiв");
            Console.WriteLine("4. Генерацiя 10000 чисел (мiн, макс, середнє)");
            Console.WriteLine("5. Завдання 4 + запис у файл");
            Console.WriteLine("0. Вихiд");
            Console.Write("Вибiр: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunTask1();
                    break;
                case "2":
                    RunTask2();
                    break;
                case "3":
                    RunTask3();
                    break;
                case "4":
                    RunTask4();
                    break;
                case "5":
                    RunTask5();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невiрний вибiр.");
                    break;
            }
        }
    }

    static void RunTask1()
    {
        Thread thread = new Thread(PrintNumbers0To50);
        thread.Start();
        thread.Join();
    }

    static void PrintNumbers0To50()
    {
        for (int i = 0; i <= 50; i++)
        {
            Console.WriteLine(i);
        }
    }

    static void RunTask2()
    {
        Console.Write("Введiть початок дiапазону: ");
        int start = int.Parse(Console.ReadLine());
        Console.Write("Введiть кiнець дiапазону: ");
        int end = int.Parse(Console.ReadLine());

        Thread thread = new Thread(() => PrintNumbers(start, end));
        thread.Start();
        thread.Join();
    }

    static void RunTask3()
    {
        Console.Write("Введiть кількiсть потокiв: ");
        int threadCount = int.Parse(Console.ReadLine());

        Console.Write("Введiть початок дiапазону: ");
        int start = int.Parse(Console.ReadLine());
        Console.Write("Введiть кiнець дiапазону: ");
        int end = int.Parse(Console.ReadLine());

        int step = (end - start + 1) / threadCount;
        Thread[] threads = new Thread[threadCount];
        int currentStart = start;

        for (int i = 0; i < threadCount; i++)
        {
            int localStart = currentStart;
            int localEnd = (i == threadCount - 1) ? end : currentStart + step - 1;

            threads[i] = new Thread(() => PrintNumbers(localStart, localEnd));
            threads[i].Start();

            currentStart += step;
        }

        foreach (var t in threads)
        {
            t.Join();
        }
    }

    static void PrintNumbers(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] {i}");
        }
    }

    static void RunTask4()
    {
        GenerateNumbers();

        Thread tMin = new Thread(FindMin);
        Thread tMax = new Thread(FindMax);
        Thread tAvg = new Thread(FindAvg);

        tMin.Start();
        tMax.Start();
        tAvg.Start();

        tMin.Join();
        tMax.Join();
        tAvg.Join();

        Console.WriteLine($"Мiнiмум: {min}");
        Console.WriteLine($"Максимум: {max}");
        Console.WriteLine($"Середнє: {avg}");
    }

    static void RunTask5()
    {
        GenerateNumbers();

        Thread tMin = new Thread(FindMin);
        Thread tMax = new Thread(FindMax);
        Thread tAvg = new Thread(FindAvg);

        tMin.Start();
        tMax.Start();
        tAvg.Start();

        tMin.Join();
        tMax.Join();
        tAvg.Join();

        Thread tFile = new Thread(WriteToFile);
        tFile.Start();
        tFile.Join();

        Console.WriteLine("Результати записано у файл results.txt");
    }

    static void GenerateNumbers()
    {
        Random rnd = new Random();

        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = rnd.Next(0, 100000);
    }

    static void FindMin()
    {
        min = int.MaxValue;

        foreach (var n in numbers)
            if (n < min) min = n;
    }

    static void FindMax()
    {
        max = int.MinValue;

        foreach (var n in numbers)
            if (n > max) max = n;
    }

    static void FindAvg()
    {
        long sum = 0;

        foreach (var n in numbers)
            sum += n;

        avg = (double)sum / numbers.Length;
    }

    static void WriteToFile()
    {
        using (StreamWriter sw = new StreamWriter("results.txt"))
        {
            sw.WriteLine("Набiр чисел:");

            foreach (var n in numbers)
                sw.Write(n + " ");

            sw.WriteLine();
            sw.WriteLine($"Мiнiмум: {min}");
            sw.WriteLine($"Максимум: {max}");
            sw.WriteLine($"Середнє: {avg}");
        }
    }
}


