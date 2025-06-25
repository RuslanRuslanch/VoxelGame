using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public sealed class VBO
    {
        public const int ElementSize = sizeof(float);

        public readonly int Id;

        public VBO(Vector3[] data, BufferUsageHint usageHint)
        {
            Id = Generate(data, usageHint);
        }

        public VBO(Vector2[] data, BufferUsageHint usageHint)
        {
            Id = Generate(data, usageHint);
        }

        public VBO(float[] data, BufferUsageHint usageHint)
        {
            Id = Generate(data, usageHint);
        }

        private int Generate(float[] data, BufferUsageHint usageHint)
        {
            var id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, usageHint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return id;
        }

        private int Generate(Vector3[] data, BufferUsageHint usageHint)
        {
            var id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * Vector3.SizeInBytes, data, usageHint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return id;
        }

        private int Generate(Vector2[] data, BufferUsageHint usageHint)
        {
            var id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * Vector2.SizeInBytes, data, usageHint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return id;
        }
    }
}
