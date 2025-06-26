using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using VoxelGame.Blocks;
using VoxelGame.GameObjects;
using VoxelGame.Inputs;

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
            if (Input.Mouse.IsButtonPressed(MouseButton.Left))
            {
                var hit = Raycast(MainCamera.Transform.Position, MainCamera.Transform.Forward, 10f);

                if (hit != null)
                {
                    hit.Chunk.SetBlock(hit.Position, BlockType.Air);
                }
            }
            if (Input.Mouse.IsButtonPressed(MouseButton.Right))
            {
                var hit = Raycast(MainCamera.Transform.Position, MainCamera.Transform.Forward, 10f);

                if (hit != null)
                {
                    hit.Chunk.SetBlock(hit.Position, BlockType.Grass);
                }
            }


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

        public RaycastHit Raycast(Vector3 start, Vector3 direction, float maxDistance)
        {
            const float stepLength = 0.1f;

            var step = direction * stepLength;
            var point = start;

            for (float i = 0; i < maxDistance; i += stepLength)
            {
                if (point.Y >= Chunk.Height || point.Y < 0)
                {
                    break;
                }

                point = Vector3.Floor(point + step);
                var blockPosition = point - new Vector3(Chunk.Width * (float)Math.Floor(point.X / Chunk.Width), 0, Chunk.Width * (float)Math.Floor(point.Z / Chunk.Width));

                if (blockPosition.X < 0)
                {
                    blockPosition.X = Chunk.Width - blockPosition.X;
                }
                if (blockPosition.Z < 0)
                {
                    blockPosition.Z = Chunk.Width - blockPosition.Z;
                }

                var chunk = GetChunk(point);
                var block = chunk.GetBlock(blockPosition);

                if (block == BlockType.Air)
                {
                    continue;
                }

                return new RaycastHit(chunk, block, blockPosition);
            }

            return null;
        }

        public Chunk GetChunk(Vector3 position)
        {
            var x = (int)Math.Floor(position.X / Chunk.Width);
            var y = (int)Math.Floor(position.Z / Chunk.Width);

            var chunkPosition = new Vector2i(x, y);

            return GetChunk(chunkPosition);
        }

        public Chunk GetChunk(Vector2i position)
        {
            return _chunkGenerator.Chunks[position];
        }
    }

    public class RaycastHit
    {
        public readonly Chunk Chunk;
        public readonly BlockType Block;
        public readonly Vector3 Position;

        public RaycastHit(Chunk chunk, BlockType block, Vector3 position)
        {
            Chunk = chunk;
            Block = block;
            Position = position;
        }
    }
}
