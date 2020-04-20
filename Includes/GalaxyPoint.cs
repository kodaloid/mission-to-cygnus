using Microsoft.Xna.Framework;

namespace ld46_entry.Includes
{
    /// <summary>
    /// Represents a point on a GalaxyGraph.
    /// </summary>
    public struct GalaxyPoint
    {
        public Vector3 Position;
        public Color Color;
        
        public GalaxyPoint(Vector3 position, Color color)
        {
            this.Position = position;
            this.Color = color;
        }

        public override string ToString()
        {
            return $"{Position} [{Color}]";
        }
    }
}