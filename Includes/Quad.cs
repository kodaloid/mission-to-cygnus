using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTC.Includes
{
    /// <summary>
    /// A structure used for rendering individual entities.
    /// </summary>
    public class Quad
    {
        public readonly int[] Indices;
        public readonly VertexPositionColorTexture[] Vertices;
        
        public Quad()
        {
            this.Indices = new int[] {
                0, 1, 2,
                2, 1, 3
            };

            Color col = Color.White;

            this.Vertices = new VertexPositionColorTexture[] {
                new VertexPositionColorTexture(new Vector3(0, 0, 0), col, new Vector2(0, 0)),
                new VertexPositionColorTexture(new Vector3(1, 0, 0), col, new Vector2(1, 0)),
                new VertexPositionColorTexture(new Vector3(0, 1, 0), col, new Vector2(0, 1)),
                new VertexPositionColorTexture(new Vector3(1, 1, 0), col, new Vector2(1, 1)),
            };
        }
    }
}