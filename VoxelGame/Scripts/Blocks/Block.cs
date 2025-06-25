using VoxelGame.Graphics;

namespace VoxelGame.Blocks
{
    public abstract class Block
    {
        public abstract BlockType Type { get;}

        public abstract UVRegion GetUV(FaceDirection direction);
    }
}
