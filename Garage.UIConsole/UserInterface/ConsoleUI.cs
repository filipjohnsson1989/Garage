namespace Garage.UIConsole.UserInterface;

public class ConsoleUI : IUI
{
    public void Clear()
    {
        //Console.CursorVisible = false;
        Console.Clear();
    }
    public ConsoleKey GetKey() => Console.ReadKey(intercept: true).Key;
    public void AddMessage(string message) => Console.WriteLine(message);

}
