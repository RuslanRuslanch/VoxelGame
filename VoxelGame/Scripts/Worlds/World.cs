using VoxelGame.Blocks;
using VoxelGame.GameObjects;

namespace VoxelGame.Worlds
{
    public sealed class World
    {
        private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

        private ChunkGenerator _chunkGenerator;

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

            var camera = new Camera(this);
            _chunkGenerator = new ChunkGenerator(camera, this);

            _chunkGenerator.Initialize();

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
            _chunkGenerator.Spawn();

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Tick();
            }
        }
    }
}
