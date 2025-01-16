using VoxelEngine.Core.Windowing;

namespace VoxelEngine;

public static class App
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Current Directory: {AppDomain.CurrentDomain.BaseDirectory}");
        
        var game = new GameWindow();
        game.Run();
    }
}