using OpenTK.Graphics.OpenGL;

namespace VoxelGame.Graphics
{
    public sealed class EBO
    {
        public const int ElementSize = sizeof(uint);

        public readonly int Id;

        public EBO(uint[] indecies)
        {
            Id = Generate(indecies);
        }

        private int Generate(uint[] indecies)
        {
            var id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indecies.Length * ElementSize, indecies, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            return id;
        }
    }
}
