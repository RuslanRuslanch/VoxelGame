using VoxelGame.Graphics;

namespace VoxelGame.Resources
{
    public static class Resource
    {
        public static Texture LoadTexture(string path)
        {
            return new Texture(path);
        }

        public static Shader LoadShader(string vertexPath, string fragmentPath)
        {
            return new Shader(vertexPath, fragmentPath);
        }
    }
}
