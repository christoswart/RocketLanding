using System;
using System.Threading.Tasks;

public interface IRocketDisplay
{
    void Clear();
    void ShowMessage(string message);
    void ShowRocket(Rocket rocket);
}

public class ConsoleRocketDisplay : IRocketDisplay
{
    public void Clear() => Console.Clear();
    public void ShowMessage(string message) => Console.WriteLine(message);
    public void ShowRocket(Rocket rocket) => Console.WriteLine(rocket.ToString());
}

public class Rocket
{
    private string rocketDisplay;
    public const string DefaultRocketArt =
        "     |\r\n     |\r\n    / \\\r\n   / _ \\\r\n  |.o '.|\r\n  |'._.'|\r\n  |     |\r\n ,'|  | |`.\r\n/  |  | |  \\\r\n|,-'--|--'-.|";

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
        if (rocketDisplay.Length >= 3)
        {
            rocketDisplay = rocketDisplay.Substring(3);
        }
        else
        {
            rocketDisplay = string.Empty;
        }
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
    private readonly IRocketDisplay _display;
    private readonly int _startCounter;
    private const int MaxLiftOffSeconds = 8;
    private const int BaseSleepTimeMs = 1100;
    private const int SleepDecrementMs = 100;
    private const string LandingMessage = "The Rocket has Landed!";
    private const string LaunchMessage = "The Rocket has Launched!";
    private const string FlyingMessage = "Flying for {0} seconds";
    private const string LandingCountdownMessage = "Landing in {0}";

    public CountdownTimer(int startCounter, Rocket rocket, IRocketDisplay display)
    {
        _counter = startCounter;
        _startCounter = startCounter;
        _rocket = rocket;
        _display = display;
    }

    public async Task StartAsync()
    {
        while (_counter >= 0)
        {
            _display.Clear();
            _display.ShowMessage(string.Format(LandingCountdownMessage, _counter));
            _display.ShowRocket(_rocket);
            _rocket.UpdateDisplay();

            int sleepTime = Math.Max(100, BaseSleepTimeMs - (_counter * SleepDecrementMs));
            await Task.Delay(sleepTime);

            _counter--;
        }
        _display.ShowMessage(LandingMessage);
    }

    public async Task LiftOffAsync()
    {
        _counter = 0;
        while (_counter < MaxLiftOffSeconds)
        {
            _display.Clear();
            _display.ShowMessage(LaunchMessage);
            _display.ShowRocket(_rocket);
            _rocket.UpdateLift();

            int sleepTime = Math.Max(100, BaseSleepTimeMs - (_counter * SleepDecrementMs));
            await Task.Delay(sleepTime);

            _counter++;
            _display.ShowMessage(string.Format(FlyingMessage, _counter));
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        Rocket rocket = new Rocket(Rocket.DefaultRocketArt);
        IRocketDisplay display = new ConsoleRocketDisplay();
        CountdownTimer timer = new CountdownTimer(10, rocket, display);
        await timer.StartAsync();
        await Task.Delay(1000);
        await timer.LiftOffAsync();
        Console.ReadKey();
    }
}