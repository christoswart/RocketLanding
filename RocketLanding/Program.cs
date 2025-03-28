﻿public class Rocket
{
    private string rocketDisplay;

    public Rocket(string initialDisplay)
    {
        rocketDisplay = initialDisplay;
    }

    public void UpdateDisplay()
    {
        rocketDisplay = "\r\n" + rocketDisplay;
    }

    public void UpdateLift()
    {
        rocketDisplay = rocketDisplay.Substring(3);
    }

    public void Display()
    {
        Console.WriteLine(rocketDisplay);
    }

    public override string ToString()
    {
        return rocketDisplay;
    }
}

public class CountdownTimer
{
    private int _counter;
    private readonly Rocket _rocket;

    public CountdownTimer(int startCounter, Rocket rocket)
    {
        _counter = startCounter;
        _rocket = rocket;
    }

    public void Start()
    {
        while (_counter >= 0)
        {
            Console.Clear();
            Console.WriteLine($"Landing in {_counter}");
            _rocket.Display();
            _rocket.UpdateDisplay();

            int sleepTime = 1100 - (_counter * 100);
            Thread.Sleep(sleepTime);

            _counter--;
        }

        Console.WriteLine("The Rocket has Landed!");
    }

    public void LiftOff()
    {
        _counter = 0;
        while (_counter < 8)
        {
            Console.Clear();
            Console.WriteLine("The Rocket has Launched!");
            _rocket.Display();
            _rocket.UpdateLift();

            int sleepTime = 1100 - (_counter * 100);
            Thread.Sleep(sleepTime);

            _counter++;
            Console.WriteLine($"Flying for {_counter} seconds");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize Rocket object
        Rocket rocket = new Rocket("     |\r\n     |\r\n    / \\\r\n   / _ \\\r\n  |.o '.|\r\n  |'._.'|\r\n  |     |\r\n ,'|  | |`.\r\n/  |  | |  \\\r\n|,-'--|--'-.|");

        // Initialize CountdownTimer with starting counter value and rocket object
        CountdownTimer timer = new CountdownTimer(10, rocket);

        // Start the countdown
        timer.Start();

        Thread.Sleep(1000);

        timer.LiftOff();

        // Wait for user input to close
        Console.ReadKey();
    }
}