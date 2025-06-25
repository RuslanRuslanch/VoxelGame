using OpenTK.Mathematics;
using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public sealed class GrassBlock : Block
    {
        public override BlockType Type => BlockType.Grass;

        public override UVRegion GetUV(FaceDirection direction)
        {
            if (direction == FaceDirection.Top)
            {
                return new UVRegion(new Vector2(16f, 0f), new Vector2(32f, 16f));
            }
            else if (direction == FaceDirection.Bottom)
            {
                return new UVRegion(new Vector2(32f, 0f), new Vector2(48f, 16f));
            }

            return new UVRegion(Vector2.Zero, new Vector2(16f, 16f));
        }
    }
}
