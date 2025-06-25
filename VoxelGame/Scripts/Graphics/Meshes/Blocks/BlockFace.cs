using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public class BlockFace
    {
        public readonly Vector3[] Vertices;
        public readonly Vector3 Normal;

        public BlockFace(Vector3[] vertices, Vector3 normal)
        {
            Vertices = vertices;
            Normal = normal;
        }
    }
}
