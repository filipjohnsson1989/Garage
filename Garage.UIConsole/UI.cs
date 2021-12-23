namespace Garage.UIConsole;

internal class UI
{
    internal static void Clear()
    {
        //Console.CursorVisible = false;
        Console.Clear();
    }
    internal static ConsoleKey GetKey() => Console.ReadKey(intercept: true).Key;
    internal static void AddMessage(string message) => Console.WriteLine(message);
}
