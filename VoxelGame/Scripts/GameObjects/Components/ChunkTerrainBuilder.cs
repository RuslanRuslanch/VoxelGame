using OpenTK.Mathematics;
using VoxelGame.Blocks;

namespace VoxelGame.GameObjects.Components
{
    public sealed class ChunkTerrainBuilder
    {
        private readonly FastNoiseLite _noise = new FastNoiseLite();
        private readonly Random _random = new Random(0);

        private BlockType[,,] _blocks;

        public ChunkTerrainBuilder()
        {
            _noise.SetFrequency(1.5f);

            _noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            _noise.SetRotationType3D(FastNoiseLite.RotationType3D.ImproveXZPlanes);
            _noise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
            _noise.SetFractalOctaves(8);
        }

        public BlockType[,,] Build(Vector3 position)
        {
            _blocks = new BlockType[Chunk.Width, Chunk.Height, Chunk.Width];

            for (var x = 0; x < Chunk.Width; x++)
            {
                for (var z = 0; z < Chunk.Width; z++)
                {
                    var height = (int)(_noise.GetNoise((position.X + x) * 0.01f, (position.Z + z) * 0.01f) * 20 + 30);
                    

                    for (var y = 0; y < height; y++)
                    {
                        if (y == 0)
                        {
                            _blocks[x, y, z] = BlockType.Stone;
                            continue;
                        }

                        var noise = _noise.GetNoise((position.X + x) * 0.05f, (position.Y + y) * 0.05f, (position.Z + z) * 0.05f) * 2;

                        if (noise > 0.75f)
                        {
                            _blocks[x, y, z] = BlockType.Air;
                            continue;
                        }

                        if (height - y <= 1)
                        {
                            _blocks[x, y, z] = BlockType.Grass;
                        }
                        else if (height - y > 1 && height - y <= 3)
                        {
                            _blocks[x, y, z] = BlockType.Dirt;
                        }
                        else
                        {
                            _blocks[x, y, z] = BlockType.Stone;
                        }

                    }

                    var treeChoice = _random.Next(-50, 50);

                    if (treeChoice >= 49)
                    {
                        GenerateTree(new Vector3i(x, height, z));
                    }
                }
            }

            return _blocks;
        }

        private bool InBounds(Vector3i position)
        {
            if (position.X >= 0 && position.X < Chunk.Width &&
                position.Y >= 0 && position.Y < Chunk.Height &&
                position.Z >= 0 && position.Z < Chunk.Width)
            {
                return true;
            }

            return false;
        }

        private void GenerateTree(Vector3i startPosition)
        {
            for (int i = 0; i < 5; i++)
            {
                _blocks[startPosition.X, startPosition.Y+i, startPosition.Z] = BlockType.Log;
            }

            for (int x = -2; x < 2; x++)
            {
                for (int y = -2; y < 2; y++)
                {
                    for (int z = -2; z < 2; z++)
                    {
                        var position = new Vector3i(startPosition.X + x, startPosition.Y + 5 + y, startPosition.Z + z);

                        var noise = _noise.GetNoise(position.X, position.Y, position.Z);

                        if (noise <= 0.01f)
                        {
                            continue;
                        }
                        if (InBounds(position) == false)
                        {
                            continue;
                        }

                        _blocks[position.X, position.Y, position.Z] = BlockType.Leaves;
                    }
                }
            }
        }
    }
}
