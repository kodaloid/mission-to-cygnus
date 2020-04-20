using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MTC.Includes
{
    /// <summary>
    /// Base class for an entity in our game.
    /// </summary>
    public class Entity
    {
        [XmlAttribute]
        public string Texture {get; set; }             // The associated texture.
        public Vector3 Trajectory { get; set; }        // The movement trajectory (gets multiplied by velocity per frame).
        [XmlAttribute]
        public float Velocity { get; set; }            // Speed of the entity (default 0.0)
        public List<Entity> Children { get; set; }     // A collection of entities that are subordinate to this one.
        public Color DiffuseColor { get; set; }        // The diffuse color of this entity.
        public Transform LocalTransform { get; set; }  // The local transform used for holding position, scale, rotation.
        [XmlIgnore]                                    // No need to serialize this as it gets recreated frequently.
        public Transform WorldTransform { get; set; }  // The calculated world transform used for creating world matrix.
        [XmlAttribute]
        public bool Visible { get; set; }              // Sprite renders only if this value is true.
        [XmlAttribute]
        public float Opacity { get; set; }             // Where 1.0 is opaque and 0.0 is translucent.


        public Entity()
        {

        }
        
        public Entity(string texture, Vector3 position)
        {
            this.Texture = texture;
            this.Velocity = 0.0f;
            this.Children = new List<Entity>();
            this.DiffuseColor = Color.White;
            this.LocalTransform = new Transform();
            this.LocalTransform.Position = position;
            this.WorldTransform = LocalTransform.Clone();
            this.Visible = true;
            this.Opacity = 1.0f;
        }
        

        public virtual void Update(GameTime gameTime)
        {
            this.Trajectory = Vector3.TransformNormal(Vector3.Down, Matrix.CreateRotationZ(this.LocalTransform.Rotation));
            this.LocalTransform.Position += Trajectory * Velocity;
        }


        public void UpdateWorldTransform(Entity parent)
        {
            this.WorldTransform = parent != null 
                                ? Transform.Compose(parent.WorldTransform, this.LocalTransform)
                                : this.LocalTransform;

            foreach (Entity child in this.Children)
            {
                child.UpdateWorldTransform(this);
            }
        }
    }
}