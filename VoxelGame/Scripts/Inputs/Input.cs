using OpenTK.Windowing.GraphicsLibraryFramework;

namespace VoxelGame.Inputs
{
    public static class Input
    {
        public static MouseState Mouse;
        public static KeyboardState Keyboard;

        public static void Initialize(MouseState mouse, KeyboardState keyboard)
        {
            Mouse = mouse;
            Keyboard = keyboard;
        }
    }
}
