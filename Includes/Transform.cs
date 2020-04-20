using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace MTC.Includes
{
    /// <summary>
    /// Describes a transformation (scale-rotation-position) of an entity.
    /// </summary>
    public class Transform
    {
        [XmlIgnore]
        public Vector3 Position { get; set; }
        [XmlAttribute("x")]
        public float X {get => Position.X; set => Position = new Vector3(value, Position.Y, Position.Z); }
        [XmlAttribute("y")]
        public float Y {get => Position.Y; set => Position = new Vector3(Position.X, value, Position.Z); }
        [XmlAttribute("z")]
        public float Z {get => Position.Z; set => Position = new Vector3(Position.X, Position.Y, value); }
        [XmlAttribute]
        public float Rotation { get; set; }
        [XmlAttribute]
        public float Scale { get; set; }


        public Transform()
        {
            this.Position = Vector3.Zero;
            this.Rotation = 0.0f;
            this.Scale = 1.0f;
        }


        /// <summary>
        /// Merge two transformations into one.
        /// </summary>
        /// <param name="a">First transformation.</param>
        /// <param name="b">Second transformation.</param>
        /// <returns>Merged transformation.</returns>
        public static Transform Compose(Transform a, Transform b)
        {
            return new Transform {
                Position = a.TransformVector(b.Position),
                Rotation = a.Rotation + b.Rotation,
                Scale = a.Scale * b.Scale
            };
        }


        public Transform Clone()
        {
            Transform ret = new Transform();
            ret.Rotation = Rotation;
            ret.Scale = Scale;
            ret.Position = Position;
            return ret;
        }


        /// <summary>
        /// Transform a vector.
        /// </summary>
        /// <param name="point">Vector to transform.</param>
        /// <returns>Transformed vector.</returns>
        public Vector3 TransformVector(Vector3 point)
        {
            Vector3 result = Vector3.Transform(point, Matrix.CreateRotationZ(Rotation * System.Math.Sign(Scale)));
            result *= Scale;
            result += Position;
            return result;
        }
    }
}