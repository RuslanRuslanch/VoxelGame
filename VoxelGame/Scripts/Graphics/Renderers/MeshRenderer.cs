using OpenTK.Graphics.OpenGL;
using VoxelGame.GameObjects;

namespace VoxelGame.Graphics
{
    public sealed class MeshRenderer : Renderer
    {
        public Mesh Mesh { get; private set; }

        public MeshRenderer(Material material, GameObject gameObject) : base(material, gameObject)
        {
        }

        public override void Render()
        {
            var shader = Material.Shader;

            var projection = GameObject.World.MainCamera.ProjectionMatrix;
            var view = GameObject.World.MainCamera.ViewMatrix;
            var model = GameObject.Transform.ModelMatrix;

            shader.Load("projection", ref projection);
            shader.Load("view", ref view);
            shader.Load("model", ref model);

            GLRenderer.Render(VAO, Mesh.VertexCount, Material.Texture.ID, shader);
        }

        public void SetMesh(Mesh mesh)
        {
            Mesh = mesh;

            RecalculateBuffers(mesh);
        }

        private void RecalculateBuffers(Mesh mesh)
        {
            DeleteOldBuffers();

            var usageHint = GameObject.IsStatic ? BufferUsageHint.StaticDraw : BufferUsageHint.DynamicDraw;

            var vbo = new VBO(Mesh.Parse(mesh), usageHint);
            var vao = new VAO(vbo.Id, Material.Shader);

            SetBuffers(vao.Id, vbo.Id);
        }

        private void DeleteOldBuffers()
        {
            if (VAO != 0)
            {
                GL.BindVertexArray(0);
                GL.DeleteVertexArray(VAO);
            }
            if (VBO != 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.DeleteBuffer(VBO);
            }
        }
    }
}
