using OpenTK.Mathematics;
using VoxelGame.Blocks;
using VoxelGame.Graphics;

namespace VoxelGame.GameObjects.Components
{
    public sealed class ChunkMeshBuilder
    {
        private BlockType[,,] _blocks;

        public Mesh Build(BlockType[,,] blocks)
        {
            var polygons = new List<Polygon>();

            _blocks = blocks;

            for (int x = 0; x < Chunk.Width; x++)
            {
                for (int y = 0; y < Chunk.Height; y++)
                {
                    for (int z = 0; z < Chunk.Width; z++)
                    {
                        var blockType = blocks[x, y, z];

                        if (blockType == BlockType.Air)
                        {
                            continue;
                        }

                        polygons.AddRange(GetBlockPolygons(blockType, new Vector3i(x, y, z)));
                    }
                }
            }

            _blocks = null;

            return new Mesh(polygons.ToArray());
        }

        private bool IsVoid(Vector3i position)
        {
            if (position.X >= 0 && position.X < Chunk.Width &&
                position.Y >= 0 && position.Y < Chunk.Height &&
                position.Z >= 0 && position.Z < Chunk.Width)
            {
                return _blocks[position.X, position.Y, position.Z] == BlockType.Air;
            }

            return false;
        }

        private Polygon[] GetBlockPolygons(BlockType type, Vector3i position)
        {
            var polygons = new List<Polygon>();
            var block = BlockCache.Get(type);

            if (IsVoid(position + Vector3i.UnitZ))
            {
                var uvRegion = block.GetUV(FaceDirection.Back);

                polygons.AddRange(GenerateBackFace(uvRegion, position));
            }
            if (IsVoid(position - Vector3i.UnitZ))
            {
                var uvRegion = block.GetUV(FaceDirection.Front);

                polygons.AddRange(GenerateFrontFace(uvRegion, position));
            }
            if (IsVoid(position + Vector3i.UnitY))
            {
                var uvRegion = block.GetUV(FaceDirection.Top);

                polygons.AddRange(GenerateTopFace(uvRegion, position));
            }
            if (IsVoid(position - Vector3i.UnitY))
            {
                var uvRegion = block.GetUV(FaceDirection.Bottom);

                polygons.AddRange(GenerateBottomFace(uvRegion, position));
            }
            if (IsVoid(position + Vector3i.UnitX))
            {
                var uvRegion = block.GetUV(FaceDirection.Right);

                polygons.AddRange(GenerateRightFace(uvRegion, position));
            }
            if (IsVoid(position - Vector3i.UnitX))
            {
                var uvRegion = block.GetUV(FaceDirection.Left);

                polygons.AddRange(GenerateLeftFace(uvRegion, position));
            }

            return polygons.ToArray();
        }

        private Polygon[] GenerateFrontFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                Vector3.Zero + position,
                Vector3.UnitY + position,
                new Vector3(1f, 1f, 0f) + position,
            };

            var vertices2 = new Vector3[]
            {
                Vector3.Zero + position,
                new Vector3(1f, 1f, 0f) + position,
                Vector3.UnitX + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Front, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, -Vector3.UnitZ);
            var polygon2 = new Polygon(vertices2, texCoords2, -Vector3.UnitZ);

            return new Polygon[] { polygon1, polygon2 };
        }

        private Polygon[] GenerateBackFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                Vector3.UnitY + Vector3.UnitZ + position,
                Vector3.Zero + Vector3.UnitZ + position,
                new Vector3(1f, 1f, 0f) + Vector3.UnitZ + position,
            };

            var vertices2 = new Vector3[]
            {
                new Vector3(1f, 1f, 0f) + Vector3.UnitZ + position,
                Vector3.Zero + Vector3.UnitZ + position,
                Vector3.UnitX + Vector3.UnitZ + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Back, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, Vector3.UnitZ);
            var polygon2 = new Polygon(vertices2, texCoords2, Vector3.UnitZ);

            return new Polygon[] { polygon1, polygon2 };
        }

        private Polygon[] GenerateTopFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                new Vector3(0f, 1f, 1f) + position,
                new Vector3(1f, 1f, 1f) + position,
                new Vector3(0f, 1f, 0f) + position,
            };

            var vertices2 = new Vector3[]
            {
                new Vector3(0f, 1f, 0f) + position,
                new Vector3(1f, 1f, 1f) + position,
                new Vector3(1f, 1f, 0f) + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Top, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, Vector3.UnitY);
            var polygon2 = new Polygon(vertices2, texCoords2, Vector3.UnitY);

            return new Polygon[] { polygon1, polygon2 };
        }

        private Polygon[] GenerateBottomFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                new Vector3(1f, 0f, 1f) + position,
                new Vector3(0f, 0f, 1f) + position,
                new Vector3(0f, 0f, 0f) + position,
            };

            var vertices2 = new Vector3[]
            {
                new Vector3(1f, 0f, 1f) + position,
                new Vector3(0f, 0f, 0f) + position,
                new Vector3(1f, 0f, 0f) + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Bottom, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, -Vector3.UnitY);
            var polygon2 = new Polygon(vertices2, texCoords2, -Vector3.UnitY);

            return new Polygon[] { polygon1, polygon2 };
        }

        private Polygon[] GenerateRightFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                new Vector3(1f, 1f, 0f) + position,
                new Vector3(1f, 1f, 1f) + position,
                new Vector3(1f, 0f, 1f) + position,
            };

            var vertices2 = new Vector3[]
            {
                new Vector3(1f, 1f, 0f) + position,
                new Vector3(1f, 0f, 1f) + position,
                new Vector3(1f, 0f, 0f) + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Right, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, Vector3.UnitX);
            var polygon2 = new Polygon(vertices2, texCoords2, Vector3.UnitX);

            return new Polygon[] { polygon1, polygon2 };
        }

        private Polygon[] GenerateLeftFace(UVRegion region, Vector3i position)
        {
            var vertices1 = new Vector3[]
            {
                new Vector3(0f, 1f, 1f) + position,
                new Vector3(0f, 1f, 0f) + position,
                new Vector3(0f, 0f, 1f) + position,
            };

            var vertices2 = new Vector3[]
            {
                new Vector3(0f, 0f, 1f) + position,
                new Vector3(0f, 1f, 0f) + position,
                new Vector3(0f, 0f, 0f) + position,
            };

            var texCoords = Tesselator.GetUVs(FaceDirection.Left, region, new Vector2i(128, 16));

            var texCoords1 = new Vector2[] { texCoords[0], texCoords[1], texCoords[2] };
            var texCoords2 = new Vector2[] { texCoords[3], texCoords[4], texCoords[5] };

            var polygon1 = new Polygon(vertices1, texCoords1, -Vector3.UnitX);
            var polygon2 = new Polygon(vertices2, texCoords2, -Vector3.UnitX);

            return new Polygon[] { polygon1, polygon2 };
        }
    }
}
