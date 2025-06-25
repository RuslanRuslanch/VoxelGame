namespace VoxelGame.Blocks
{
    public static class BlockCache
    {
        private static Dictionary<BlockType, Block> _cache = new Dictionary<BlockType, Block>();

        public static void Initialize()
        {
            Register(new GrassBlock());
            Register(new StoneBlock());
            Register(new DirtBlock());
            Register(new LogBlock());
            Register(new LeavesBlock());
        }

        public static Block Get(BlockType blockType)
        {
            return _cache[blockType];
        }

        public static void Register(Block block)
        {
            _cache.Add(block.Type, block);
        }

        public static void Unregister(BlockType block)
        {
            _cache.Remove(block);
        }
    }
}
