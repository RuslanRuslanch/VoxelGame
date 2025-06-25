using VoxelGame.Worlds;

namespace VoxelGame.GameObjects
{
    public sealed class Player : GameObject
    {
        public const int RenderRadius = 8;
        public const int LoadRadius = RenderRadius + 1;

        public Player(World world, bool isStatic = false) : base(world, isStatic)
        {
        }
    }
}
