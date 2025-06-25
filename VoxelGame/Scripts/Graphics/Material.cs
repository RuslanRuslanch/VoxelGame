﻿namespace VoxelGame.Graphics
{
    public sealed class Material
    {
        public readonly Texture Texture;
        public readonly Shader Shader;

        public Material(Texture texture, Shader shader)
        {
            Texture = texture;
            Shader = shader;
        }
    }
}
