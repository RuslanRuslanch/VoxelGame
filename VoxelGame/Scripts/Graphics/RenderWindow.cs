using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using VoxelGame.Inputs;
using VoxelGame.Worlds;

namespace VoxelGame.Graphics
{
    public sealed class RenderWindow : GameWindow
    {
        private readonly World _world;
        private readonly TickSystem _tickSystem;

        public RenderWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _world = new World();
            _tickSystem = new TickSystem(_world);

            //VSync = VSyncMode.On;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GLRenderer.Initialize(this);
            Input.Initialize(MouseState, KeyboardState);

            _world.Load();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _world.Unload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(Color4.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);

            _world.Render();

            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _tickSystem.Calculate((float)args.Time);

            _world.Update((float)args.Time);
            _tickSystem.Tick();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}
