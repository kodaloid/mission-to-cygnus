using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTC.Includes.Renderers
{
    /// <summary>
    /// A render engine for rendering the play scene.
    /// </summary>
    public class PlayRenderer
    {
        public GraphicsDevice Device { get; set; }
        public Quad Quad { get; set; }
        public BasicEffect Effect { get; set; }


        public PlayRenderer(GraphicsDevice device)
        {
            this.Device = device;
            this.Quad = new Quad();
            this.Effect = new BasicEffect(device);
            this.Effect.VertexColorEnabled = true;
            this.Effect.TextureEnabled = true;
        }


        public void SetCamera(Camera camera)
        {
            Viewport viewport = Device.Viewport;
            Vector3 cameraUp = Vector3.Transform(Vector3.Down, Matrix.CreateRotationZ(camera.Rotation));

            this.Effect.View = Matrix.CreateLookAt(camera.Position, camera.Position +  Vector3.UnitZ, cameraUp)
                             * Matrix.CreateScale(MathHelper.Clamp(camera.Zoom, 0.01f, 3f));
            this.Effect.Projection = Matrix.CreateOrthographic(viewport.Width, viewport.Height, 0, 1);
        }


        /// <summary>
        /// Draw an entity using it's texture and transform information.
        /// </summary>
        public void DrawEntity(Entity entity, Entity parent = null)
        {
            var tex = Game1.CurrentGame.Textures[entity.Texture];
            var w = tex.Width;
            var h = tex.Height;

            // create world transformations (merged with parent)
            entity.UpdateWorldTransform(parent);
            var l = entity.LocalTransform;
            var t = entity.WorldTransform;

            Matrix world = Matrix.CreateTranslation(-0.5f, -0.5f, 0)  // this makes 0,0 the pivot
                         * Matrix.CreateScale(w * l.Scale, h * l.Scale, 1)
                         * Matrix.CreateRotationZ(t.Rotation)    // rotate
                         * Matrix.CreateTranslation(t.Position); // move it

            this.Effect.World = world;
            this.Effect.Texture = tex;
            this.Effect.DiffuseColor = entity.DiffuseColor.ToVector3();

            foreach (EffectPass pass in this.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, Quad.Vertices, 0, 4, Quad.Indices, 0, 2);
            }

            foreach (Entity child in entity.Children)
            {
                DrawEntity(child, entity);
            }
        }


        /// <summary>
        /// Draw a static mesh (triangle list).
        /// </summary>
        public void DrawStaticMesh(StaticMesh mesh) => DrawStaticMesh(mesh, Matrix.Identity);


        /// <summary>
        /// Draw a transformed static mesh (triangle list).
        /// </summary>
        public void DrawStaticMesh(StaticMesh mesh, Matrix transform)
        {
            float w = mesh.Texture.Width;
            float h = mesh.Texture.Height;

            this.Effect.World = transform;
            this.Effect.Texture = mesh.Texture;
            this.Effect.DiffuseColor = Vector3.One;

            int vertexCount = mesh.Vertices.Length;

            foreach (EffectPass pass in this.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, mesh.Vertices, 0, vertexCount, mesh.Indices, 0, mesh.PrimitiveCount);
            }
        }
    }
}