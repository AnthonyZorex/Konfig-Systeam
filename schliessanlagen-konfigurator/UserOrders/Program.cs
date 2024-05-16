using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Создаем таймер, который будет запускать функцию каждую минуту
        Timer timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1.0));


        timer.Dispose();
    }

    static void TimerCallback(object state)
    {
       
        Console.WriteLine($"Текущее время: {DateTime.Now}");
    }
}