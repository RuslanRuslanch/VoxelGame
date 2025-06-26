using OpenTK.Mathematics;
using VoxelGame.Blocks;
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

        public readonly BlockType[,,] Blocks = new BlockType[Width, Height, Width];

        public Chunk(World world, bool isStatic = true) : base(world, isStatic)
        {

        }

        public override void Load()
        {
            _renderer = new MeshRenderer(_material, this);

            GenerateTerrain();
            GenerateMesh();
        }

        public void Initialize(Material material, ChunkTerrainBuilder terrainBuilder, ChunkMeshBuilder meshBuilder)
        {
            _terrainBuilder = terrainBuilder;
            _meshBuilder = meshBuilder;
            _material = material;
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

        public void SetBlock(Vector3 position, BlockType type)
        {
            Blocks[(int)position.X, (int)position.Y, (int)position.Z] = type;

            GenerateMesh();
        }

        public void SetBlock(BlockType[,,] types)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int z = 0; z < Width; z++)
                    {
                        Blocks[x, y, z] = types[x, y, z];
                    }
                }
            }

            GenerateMesh();
        }

        public BlockType GetBlock(Vector3 position)
        {
            return Blocks[(int)position.X, (int)position.Y, (int)position.Z];
        }

        public void GenerateTerrain()
        {
            var blocks = _terrainBuilder.Build(Transform.Position);

            SetBlock(blocks);
        }

        public void GenerateMesh()
        {
            var mesh = _meshBuilder.Build(Blocks);

            SetMesh(mesh);
        }

        public void SetMesh(Mesh mesh)
        {
            _renderer.SetMesh(mesh);
        }
    }
}
