namespace VoxelGame.Graphics
{
    public abstract class BlockMesh
    {
        public abstract BlockFace[] GetFrontFace();
        public abstract BlockFace[] GetBackFace();
        public abstract BlockFace[] GetTopFace();
        public abstract BlockFace[] GetBottomFace();
        public abstract BlockFace[] GetRightFace();
        public abstract BlockFace[] GetLeftFace();
    }
}
