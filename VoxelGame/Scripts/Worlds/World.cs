using OpenTK.Mathematics;
using VoxelGame.Blocks;
using VoxelGame.GameObjects;
using VoxelGame.GameObjects.Components;
using VoxelGame.Graphics;
using VoxelGame.Resources;

namespace VoxelGame.Worlds
{
    public sealed class World
    {
        private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

        public Camera MainCamera { get; private set; }

        public void Register(GameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
            {
                return;
            }

            if (gameObject is Camera camera)
            {
                MainCamera = camera;
            }

            gameObject.Load();

            _gameObjects.Add(gameObject);
        }

        public void Unregister(GameObject gameObject)
        {
            gameObject.Unload();

            _gameObjects.Remove(gameObject);
        }

        public void Load()
        {
            BlockCache.Initialize();

            var terrainBuilder = new ChunkTerrainBuilder();
            var meshBuilder = new ChunkMeshBuilder();

            var texture = Resource.LoadTexture(@"Contents\Textures\TextureAtlas.png");
            var shader = Resource.LoadShader(@"Contents\Shaders\BlockVertexShader.vert", @"Contents\Shaders\BlockFragmentShader.frag");
            var material = new Material(texture, shader);

            for (int x = -16; x < 16; x++)
            {
                for (int z = -16; z < 16; z++)
                {
                    var chunk = new Chunk(this);

                    chunk.Initialize(material, terrainBuilder, meshBuilder);

                    chunk.Transform.SetPosition(new Vector3(x, 0f, z) * Chunk.Width);

                    Register(chunk);
                }
            }

            var camera = new Camera(this);

            Register(camera);
        }

        public void Unload()
        {
            
        }

        public void Update(float deltaTime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }
        
        public void Render()
        {
            foreach (var gameObject in _gameObjects)
            {
                if (gameObject.CanRender(MainCamera.Frustum) == false)
                {
                    continue;
                }

                gameObject.Render();
            }
        }

        public void Tick()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Tick();
            }
        }
    }
}
