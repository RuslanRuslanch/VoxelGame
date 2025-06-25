using OpenTK.Mathematics;
using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public sealed class DirtBlock : Block
    {
        public override BlockType Type => BlockType.Dirt;

        public override UVRegion GetUV(FaceDirection direction)
        {
            return new UVRegion(new Vector2(32f, 0f), new Vector2(48f, 16f));
        }
    }
}
