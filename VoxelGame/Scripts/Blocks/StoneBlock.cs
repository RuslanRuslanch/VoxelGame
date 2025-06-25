using OpenTK.Mathematics;
using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public sealed class StoneBlock : Block
    {
        public override BlockType Type => BlockType.Stone;

        public override UVRegion GetUV(FaceDirection direction)
        {
            return new UVRegion(new Vector2(48f, 0f), new Vector2(64f, 16f));
        }
    }
}
