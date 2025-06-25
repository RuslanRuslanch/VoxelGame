using VoxelGame.GameObjects;

namespace VoxelGame.Graphics
{
    public abstract class Renderer
    {
        public Material Material { get; private set; }
        public GameObject GameObject { get; private set; }

        public int VAO { get; private set; }
        public int VBO { get; private set; }

        public Renderer(Material material, GameObject gameObject)
        {
            GameObject = gameObject;

            SetMaterial(material);
        }

        public abstract void Render();

        public void SetBuffers(int vao, int vbo)
        {
            VAO = vao;
            VBO = vbo;
        }

        public void SetMaterial(Material material)
        {
            Material = material;
        }
    }
}
