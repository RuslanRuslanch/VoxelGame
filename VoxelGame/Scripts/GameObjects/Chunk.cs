using OpenTK.Mathematics;
using VoxelGame.GameObjects.Components;
using VoxelGame.Graphics;
using VoxelGame.Worlds;

namespace VoxelGame.GameObjects
{
    public sealed class Chunk : GameObject
    {
        public const int Width = 16;
        public const int Height = 256;

        private MeshRenderer _renderer;
        private ChunkTerrainBuilder _terrainBuilder;
        private ChunkMeshBuilder _meshBuilder;
        private Material _material;

        public Chunk(World world, bool isStatic = true) : base(world, isStatic)
        {

        }

        public void Initialize(Material material, ChunkTerrainBuilder terrainBuilder, ChunkMeshBuilder meshBuilder)
        {
            _terrainBuilder = terrainBuilder;
            _meshBuilder = meshBuilder;
            _material = material;
        }

        public override void Load()
        {
            var blocks = _terrainBuilder.Build(Transform.Position);
            var mesh = _meshBuilder.Build(blocks);

            _renderer = new MeshRenderer(_material, this);
            _renderer.SetMesh(mesh);
        }

        public override bool CanRender(Frustum frustum)
        {
            var aabb = new AABB(Transform.Position, Transform.Position + new Vector3(Width, Height, Width));

            return frustum.InFrustum(aabb);
        }

        public override void Render()
        {
            _renderer.Render();
        }
    }
}
