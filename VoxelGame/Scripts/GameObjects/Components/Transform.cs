using OpenTK.Mathematics;

namespace VoxelGame.GameObjects.Components
{
    public sealed class Transform
    {
        public Matrix4 ModelMatrix { get; private set; } = Matrix4.Identity;

        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }
        public Vector3 Scale { get; private set; } = Vector3.One;

        public Vector3 Forward { get; private set; } = -Vector3.UnitZ;
        public Vector3 Up { get; private set; } = Vector3.UnitY;
        public Vector3 Right { get; private set; } = Vector3.UnitX;

        public Transform()
        {
            LoadModelMatrix();
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;

            LoadModelMatrix();
        }

        public void SetRotation(Vector3 rotation)
        {
            Rotation = rotation;

            LoadModelMatrix();
            RecalculateDirections();

            Console.WriteLine($"Forward: {Forward}");
        }

        public void SetScale(Vector3 scale)
        {
            Scale = scale;

            LoadModelMatrix();
        }

        public void Move(Vector3 velocity)
        {
            Position += velocity;

            LoadModelMatrix();
        }

        public void Rotate(Vector3 angle)
        {
            Rotation += angle;

            LoadModelMatrix();
            RecalculateDirections();

            Console.WriteLine($"Forward: {Forward}");
        }

        private void LoadModelMatrix()
        {
            var rotationX = MathHelper.DegreesToRadians(Rotation.X);
            var rotationY = MathHelper.DegreesToRadians(Rotation.Y);
            var rotationZ = MathHelper.DegreesToRadians(Rotation.Z);

            ModelMatrix =
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateRotationZ(rotationZ) *
                Matrix4.CreateRotationY(rotationY) *
                Matrix4.CreateRotationX(rotationX) *
                Matrix4.CreateTranslation(Position);
        }

        private void RecalculateDirections()
        {
            var cosX = (float)Math.Cos(Rotation.X);
            var sinX = (float)Math.Sin(Rotation.X);
            var cosY = (float)Math.Cos(Rotation.Y);
            var sinY = (float)Math.Sin(Rotation.Y);

            var forward = new Vector3(
                sinY * cosX,
                sinX,
                -cosY * cosX
            );

            Forward = Vector3.Normalize(forward);
            Right = Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Forward));
        }
    }
}
