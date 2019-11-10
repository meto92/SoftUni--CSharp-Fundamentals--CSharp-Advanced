using System;
using System.Collections.Generic;

class TrafficLight
{
    static void Main(string[] args)
    {
        int carsThatCanPassCount = int.Parse(Console.ReadLine());

        Queue<string> cars = new Queue<string>();

        string input;
        int passedCarsCount = 0;

        while ((input = Console.ReadLine()) != "end")
        {
            if (input == "green")
            {
                for (int i = 0; i < carsThatCanPassCount && cars.Count > 0; i++)
                {
                    passedCarsCount++;
                    Console.WriteLine($"{cars.Dequeue()} passed!");
                }
            }
            else
            {
                cars.Enqueue(input);
            }
        }

        Console.WriteLine($"{passedCarsCount} cars passed the crossroads.");
    }
}