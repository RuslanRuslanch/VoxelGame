using OpenTK.Mathematics;
using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public sealed class LogBlock : Block
    {
        public override BlockType Type => BlockType.Log;

        public override UVRegion GetUV(FaceDirection direction)
        {
            if (direction == FaceDirection.Top ||
                direction == FaceDirection.Bottom)
            {
                return new UVRegion(new Vector2(80f, 0f), new Vector2(96f, 16f));
            }

            return new UVRegion(new Vector2(64f, 0f), new Vector2(80f, 16f));
        }
    }
}
