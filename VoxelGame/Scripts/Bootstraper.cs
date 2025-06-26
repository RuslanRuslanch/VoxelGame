using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using VoxelGame.Blocks;
using VoxelGame.Graphics;

namespace VoxelGame
{
    public sealed class Bootstraper
    {
        public void Initialize()
        {
            BlockCache.Initialize();

            InitializeWindow();
        }

        private void InitializeWindow()
        {
            var gameSettings = new GameWindowSettings();
            var nativeSettings = new NativeWindowSettings();

            nativeSettings.Profile = ContextProfile.Core;

            using (var window = new RenderWindow(gameSettings, nativeSettings))
            {
                GLRenderer.Initialize(window);

                window.Run();
            }
        }
    }
}
