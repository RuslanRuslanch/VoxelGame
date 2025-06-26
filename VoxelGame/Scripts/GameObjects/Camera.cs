using OpenTK.Mathematics;
using OpenTK.Platform.Windows;
using OpenTK.Windowing.GraphicsLibraryFramework;
using VoxelGame.Graphics;
using VoxelGame.Inputs;
using VoxelGame.Worlds;

namespace VoxelGame.GameObjects
{
    public sealed class Camera : GameObject
    {
        public const float ZNear = 0.01f;
        public const float ZFar = 1000.0f;

        public Matrix4 ProjectionMatrix { get; private set; } = Matrix4.Identity;
        public Matrix4 ViewMatrix { get; private set; } = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix { get; private set; } = Matrix4.Identity;

        public float FOV { get; private set; } = 90f;

        public Frustum Frustum { get; private set; }
        
        public Camera(World world) : base(world)
        {

        }

        public override void Load()
        {
            LoadProjectionMatrix();
            LoadViewMatrix();
            LoadViewProjectionMatrix();
            LoadFrustum();

            Console.WriteLine($"Forward: {Transform.Forward}");
        }

        public override void Update(float deltaTime)
        {
            UpdateMovement(deltaTime);
            
            LoadViewMatrix();
            LoadViewProjectionMatrix();

            Frustum.RecalculatePlanes();
        }

        private void LoadFrustum()
        {
            Frustum = new Frustum(this);

            Frustum.Initialize();
        }

        private void UpdateMovement(float delta)
        {
            var direction = Vector3.Zero;
            var rotation = 0f;

            if (Input.Keyboard.IsKeyDown(Keys.W))
            {
                direction += Transform.Forward;
            }
            if (Input.Keyboard.IsKeyDown(Keys.S))
            {
                direction -= Transform.Forward;
            }

            if (Input.Keyboard.IsKeyDown(Keys.D))
            {
                direction += Transform.Right;
            }
            if (Input.Keyboard.IsKeyDown(Keys.A))
            {
                direction -= Transform.Right;
            }

            if (Input.Keyboard.IsKeyDown(Keys.Space))
            {
                direction += Transform.Up;
            }
            if (Input.Keyboard.IsKeyDown(Keys.LeftShift))
            {
                direction -= Transform.Up;
            }

            if (Input.Keyboard.IsKeyDown(Keys.Left))
            {
                rotation -= 10f;
            }
            if (Input.Keyboard.IsKeyDown(Keys.Right))
            {
                rotation += 10f;
            }

            Transform.Move(direction * (15f * delta));
            Transform.Rotate(Vector3.UnitY * (rotation * delta));
        }

        public void SetFOV(float fov)
        {
            FOV = fov;
        }

        public void LoadProjectionMatrix()
        {
            var fov = MathHelper.DegreesToRadians(FOV);
            var aspect = (float)GLRenderer.Window.Size.X / GLRenderer.Window.Size.Y;

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, ZNear, ZFar);
        }

        public void LoadViewProjectionMatrix()
        {
            ViewProjectionMatrix = ViewMatrix * ProjectionMatrix;
        }

        public void LoadViewMatrix()
        {
            ViewMatrix = Matrix4.LookAt(Transform.Position, Transform.Position + Transform.Forward, Transform.Up);
        }
    }
}
