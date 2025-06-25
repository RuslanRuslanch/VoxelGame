using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public struct UVRegion
    {
        public readonly Vector2 Min = Vector2.Zero;
        public readonly Vector2 Max = Vector2.One;

        public UVRegion(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }
    }
}
