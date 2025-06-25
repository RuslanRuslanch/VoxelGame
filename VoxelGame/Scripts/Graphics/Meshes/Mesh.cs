using OpenTK.Mathematics;

namespace VoxelGame.Graphics
{
    public sealed class Mesh
    {
        public readonly Polygon[] Polygons;
        public readonly int VertexCount;

        public Mesh(Polygon[] polygons)
        {
            Polygons = polygons;
            VertexCount = polygons.Length * 3;
        }

        public static float[] Parse(Mesh mesh)
        {
            var data = new List<float>();

            foreach (var polygon in mesh.Polygons)
            {
                for (int i = 0; i < polygon.Vertices.Length; i++)
                {
                    data.Add(polygon.Vertices[i].X);
                    data.Add(polygon.Vertices[i].Y);
                    data.Add(polygon.Vertices[i].Z);

                    data.Add(polygon.TexCoords[i].X);
                    data.Add(polygon.TexCoords[i].Y);

                    data.Add(polygon.Normal.X);
                    data.Add(polygon.Normal.Y);
                    data.Add(polygon.Normal.Z);
                }
            }

            return data.ToArray();
        }
    }

    public sealed class Polygon
    {
        public readonly Vector3[] Vertices;
        public readonly Vector2[] TexCoords;
        public readonly Vector3 Normal;

        public Polygon(Vector3[] vertices, Vector2[] texCoords, Vector3 normal)
        {
            Vertices = vertices;
            TexCoords = texCoords;
            Normal = normal;
        }
    }
}
