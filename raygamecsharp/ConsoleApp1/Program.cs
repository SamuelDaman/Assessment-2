using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    /// <summary>
    /// This class initializes and runs the application.
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            InitWindow(1500, 900, "Tanks for Everything!");

            SetTargetFPS(60);

            game.Init();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
                game.CollisionDetection();
            }

            game.Shutdown();

            CloseWindow();
        }
    }
}
