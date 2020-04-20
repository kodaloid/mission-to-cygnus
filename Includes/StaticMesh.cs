using Microsoft.Xna.Framework.Graphics;

namespace ld46_entry.Includes
{
    /// <summary>
    /// A flat mesh that can be drawn by the renderer we built.
    /// </summary>
    public class StaticMesh
    {
        public Texture2D Texture {get; }
        public VertexPositionColorTexture[] Vertices { get; }
        public int[] Indices { get; }
        public int PrimitiveCount { get; }

        public StaticMesh(Texture2D texture, VertexPositionColorTexture[] vertices, int[] indices)
        {
            this.Texture = texture;
            this.Vertices = vertices;
            this.Indices = indices;
            this.PrimitiveCount = indices.Length / 3;
        }
    }
}