using OpenTK.Graphics.OpenGL;

namespace VoxelGame.Graphics
{
    public sealed class VAO
    {
        public const int Stride = 8 * sizeof(float);

        public const int Offset1 = 3 * sizeof(float);
        public const int Offset2 = 5 * sizeof(float);

        public readonly int Id;

        public VAO(int vbo, Shader shader)
        {
            Id = Generate(vbo, shader);
        }

        // vertex, texCoords, normals
        private int Generate(int vbo, Shader shader)
        {
            var id = GL.GenVertexArray();

            var positionIndex = shader.GetLocation("vPosition");
            var texCoordsIndex = shader.GetLocation("vTexCoords");
            var normalIndex = shader.GetLocation("vNormal");

            GL.BindVertexArray(id);

            GL.EnableVertexAttribArray(positionIndex);
            GL.EnableVertexAttribArray(texCoordsIndex);
            GL.EnableVertexAttribArray(normalIndex);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(positionIndex, 3, VertexAttribPointerType.Float, false, Stride, 0);
            GL.VertexAttribPointer(texCoordsIndex, 2, VertexAttribPointerType.Float, false, Stride, Offset1);
            GL.VertexAttribPointer(normalIndex, 3, VertexAttribPointerType.Float, false, Stride, Offset2);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableVertexAttribArray(positionIndex);
            GL.DisableVertexAttribArray(texCoordsIndex);
            GL.DisableVertexAttribArray(normalIndex);

            return id;
        }
    }
}
