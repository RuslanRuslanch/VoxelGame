using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public class AABB
    {
        public readonly Vector3 Min;
        public readonly Vector3 Max;

        public AABB()
        {
            Min = Vector3.Zero;
            Max = Vector3.One;
        }

        public AABB(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }
    }
}

