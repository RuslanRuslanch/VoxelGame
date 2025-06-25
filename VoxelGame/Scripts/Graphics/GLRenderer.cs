
using OpenTK.Graphics.OpenGL;

namespace VoxelGame.Graphics
{
    public static class GLRenderer
    {
        public static RenderWindow Window { get; private set; }

        private static int _texture;
        private static int _shader;

        public static void Initialize(RenderWindow window)
        {
            Window = window;
        }

        public static void Render(int vao, int vertexCount, int texture, Shader shader)
        {
            BindTexture(texture);
            BindShader(shader);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
        }

        private static void BindTexture(int texture)
        {
            if (_texture == texture)
            {
                return;
            }

            _texture = texture;

            GL.BindTexture(TextureTarget.Texture2D, texture);
        }

        private static void BindShader(Shader shader)
        {
            if (_shader == shader.ID)
            {
                return;
            }

            _shader = shader.ID;

            shader.Enable();
        }
    }
}
