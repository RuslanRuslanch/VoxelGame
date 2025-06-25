using OpenTK.Mathematics;
using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public sealed class LeavesBlock : Block
    {
        public override BlockType Type => BlockType.Leaves;

        public override UVRegion GetUV(FaceDirection direction)
        {
            return new UVRegion(new Vector2(96f, 0f), new Vector2(112f, 16f));
        }
    }
}
