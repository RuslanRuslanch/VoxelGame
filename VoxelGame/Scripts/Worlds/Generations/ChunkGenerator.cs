using OpenTK.Mathematics;
using VoxelGame.Blocks;
using VoxelGame.GameObjects;
using VoxelGame.GameObjects.Components;
using VoxelGame.Graphics;
using VoxelGame.Resources;

namespace VoxelGame.Worlds
{
    public sealed class ChunkGenerator
    {
        public readonly Dictionary<Vector2i, Chunk> Chunks = new Dictionary<Vector2i, Chunk>();
        
        private readonly Camera _camera;
        private readonly World _world;

        public Vector2i PlayerChunkPosition => new Vector2i(
            (int)(_camera.Transform.Position.X / Chunk.Width),
            (int)(_camera.Transform.Position.Z / Chunk.Width));


        private Vector2i _lastPlayerChunkPosition = new Vector2i(-1000, 1000);
        private Material _material;
        private ChunkTerrainBuilder _terrainBuilder;
        private ChunkMeshBuilder _meshBuilder;

        public ChunkGenerator(Camera camera, World world)
        {
            _camera = camera;
            _world = world;
        }

        public void Initialize()
        {
            var terrainBuilder = new ChunkTerrainBuilder();
            var meshBuilder = new ChunkMeshBuilder();

            var texture = Resource.LoadTexture(@"Contents\Textures\TextureAtlas.png");
            var shader = Resource.LoadShader(@"Contents\Shaders\BlockVertexShader.vert", @"Contents\Shaders\BlockFragmentShader.frag");
            var material = new Material(texture, shader);

            _material = material;
            _terrainBuilder = terrainBuilder;
            _meshBuilder = meshBuilder;
        }

        public void Spawn()
        {
            var currentChunkPosition = PlayerChunkPosition;

            if (currentChunkPosition == _lastPlayerChunkPosition)
            {
                return;
            }

            _lastPlayerChunkPosition = currentChunkPosition;

            SpawnChunks();
        }

        private void SpawnChunks()
        {
            for (int x = -Player.LoadRadius; x < Player.LoadRadius; x++)
            {
                for (int y = -Player.LoadRadius; y < Player.LoadRadius; y++)
                {
                    var chunkPosition = new Vector2i(x, y) + _lastPlayerChunkPosition;

                    if (Chunks.ContainsKey(chunkPosition))
                    {
                        continue;
                    }

                    var chunk = SpawnChunk(chunkPosition);

                    Chunks.Add(chunkPosition, chunk);
                    _world.Register(chunk);
                }
            }
        }

        private Chunk SpawnChunk(Vector2i position)
        {
            var chunk = new Chunk(_world);

            chunk.Transform.SetPosition(new Vector3(position.X, 0f, position.Y) * Chunk.Width);

            chunk.Initialize(_material, _terrainBuilder, _meshBuilder);

            return chunk;
        }
    }
}
