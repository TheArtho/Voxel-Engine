using VoxelEngine.Core.Windowing;

namespace VoxelEngine;

public static class App
{
    public static void Main(string[] args)
    {
        var game = new GameWindow();
        game.Run();
    }
}