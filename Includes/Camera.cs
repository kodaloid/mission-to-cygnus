using Microsoft.Xna.Framework;

namespace MTC.Includes
{
    /// <summary>
    /// An object used for tracking the location of the player through space.
    /// </summary>
    public class Camera
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }


        public Camera(Vector3 startPosition)
        {
            this.Position = startPosition;
            this.Rotation = 0.0f;
            this.Zoom = 1.0f;
        }


        public void AlignTo(Entity entity)
        {
            this.Position = entity.LocalTransform.Position;
            this.Rotation = entity.LocalTransform.Rotation;
        }
    }
}