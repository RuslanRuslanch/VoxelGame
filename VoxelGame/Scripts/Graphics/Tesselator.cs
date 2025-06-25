using OpenTK.Mathematics;
using VoxelGame.Blocks;

namespace VoxelGame.Graphics
{
    public static class Tesselator
    {
        public static Vector2[] GetUVs(FaceDirection faceDirection, UVRegion region, Vector2i textureSize)
        {
            var texCoords = Array.Empty<Vector2>();

            var uvZero = new Vector2(region.Min.X / textureSize.X, region.Min.Y / textureSize.Y);
            var uvX = new Vector2(region.Max.X / textureSize.X, region.Min.Y / textureSize.Y);
            var uvY = new Vector2(region.Min.X / textureSize.X, region.Max.Y / textureSize.Y);
            var uvOne = new Vector2(region.Max.X / textureSize.X, region.Max.Y / textureSize.Y);

            if (faceDirection == FaceDirection.Top)
            {
                texCoords = new Vector2[]
                {
                    uvY,
                    uvOne,
                    uvZero,

                    uvZero,
                    uvOne,
                    uvX,
                };
            }
            else if (faceDirection == FaceDirection.Bottom)
            {
                texCoords = new Vector2[]
                {
                    uvOne,
                    uvY,
                    uvZero,

                    uvOne,
                    uvZero,
                    uvX,
                };
            }
            else if (faceDirection == FaceDirection.Right)
            {
                texCoords = new Vector2[]
                {
                    uvY,
                    uvOne,
                    uvX,

                    uvY,
                    uvX,
                    uvZero,
                };
            }
            else if (faceDirection == FaceDirection.Left)
            {
                texCoords = new Vector2[]
                {
                    uvOne,
                    uvY,
                    uvX,

                    uvX,
                    uvY,
                    uvZero,
                };
            }
            else if (faceDirection == FaceDirection.Front)
            {
                texCoords = new Vector2[]
                {
                    uvZero,
                    uvY,
                    uvOne,

                    uvZero,
                    uvOne,
                    uvX,
                };
            }
            else if (faceDirection == FaceDirection.Back)
            {
                texCoords = new Vector2[]
                {
                    uvY,
                    uvZero,
                    uvOne,

                    uvOne,
                    uvZero,
                    uvX,
                };
            }

            return texCoords;
        }
    }
}
