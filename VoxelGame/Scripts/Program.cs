using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using VoxelGame.Graphics;

namespace VoxelGame
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            var gameSettings = new GameWindowSettings();
            var nativeSettings = new NativeWindowSettings();

            nativeSettings.Profile = ContextProfile.Core;

            using (var window = new RenderWindow(gameSettings, nativeSettings))
            {
                window.Run();
            }
        }
    }
}
