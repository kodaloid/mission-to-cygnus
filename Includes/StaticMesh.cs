using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MTC.Includes
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


    /// <summary>
    /// A utility class used for building static meshes.
    /// </summary>
    public class StaticMeshBuilder
    {
        private int _iter;
        private readonly Vector2[] _texCoords;
        public List<VertexPositionColorTexture> Vertices { get; private set; }
        public List<int> Indices { get; private set; }


        public StaticMeshBuilder()
        {
            _iter = 0;
            _texCoords = new Vector2[] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            Vertices = new List<VertexPositionColorTexture>();
            Indices = new List<int>();
        }


        /// <summary>
        /// Add a billboard/quad to this mesh.
        /// This is messy atm I know! just want to make sure it works first.
        /// </summary>
        public void AddQuad(Vector3 location, float width, float height, Color color, float scale = 1.0f)
        {
            width = width * scale;
            height = height * scale;

            float h_width = width * 0.5f;
            float h_height = height * 0.5f;
            
            Vector3 tl = new Vector3(location.X - h_width, location.Y - h_height, location.Z);
            Vector3 tr = new Vector3(location.X + h_width, location.Y - h_height, location.Z);
            Vector3 bl = new Vector3(location.X - h_width, location.Y + h_height, location.Z);
            Vector3 br = new Vector3(location.X + h_width, location.Y + h_height, location.Z);

            Vertices.AddRange(new VertexPositionColorTexture[] { 
                new VertexPositionColorTexture(tl, color, _texCoords[0]),
                new VertexPositionColorTexture(tr, color, _texCoords[1]),
                new VertexPositionColorTexture(bl, color, _texCoords[2]),
                new VertexPositionColorTexture(br, color, _texCoords[3])
            });

            Indices.AddRange(new int[] {
                _iter + 0, _iter + 1, _iter + 2,
                _iter + 2, _iter + 1, _iter + 3
            });

            _iter += 4;
        }


        /// <summary>
        /// Convert the stored data in this instance into a renderable mesh.
        /// </summary>
        public StaticMesh ToMesh(Texture2D texture)
        {
            return new StaticMesh(texture, Vertices.ToArray(), Indices.ToArray());
        }
    }
}