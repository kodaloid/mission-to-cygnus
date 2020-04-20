using Microsoft.Xna.Framework;
using MTC.Includes.Renderers;
using System;

namespace MTC.Includes
{
    /// <summary>
    /// A renderable star field that uses the GalaxyGraph class to generate coords.
    /// </summary>
    public class StarField
    {
        public StaticMesh[] PointMeshes { get; }
        public float[] Velocities { get; }
        public float[] Rotations { get; }
        public Entity Singularity { get; } // a void at the centre of nothingness.


        public StarField(int layerCount)
        {
            var random = new Random();
            var game = Game1.CurrentGame;

            PointMeshes = new StaticMesh[layerCount];
            Velocities = new float[layerCount];
            Rotations = new float[layerCount];

            var galaxyGraph = new GalaxyGraph();

            for (int i=0; i<layerCount; i++) {
                galaxyGraph.GeneratePoints(15000); // always new.
                PointMeshes[i] = galaxyGraph.ToStaticMesh(game.GraphicsDevice, "pointTexture");
                Velocities[i] = 0.0002f + (float)(random.NextDouble() * 0.0002);
            }

            Singularity = new Entity("starTexture", Vector3.Zero);
            Singularity.LocalTransform.Scale = 7.5f;
            Singularity.DiffuseColor = Color.Black;
        }


        public void Update(GameTime gameTime)
        {
            for (int i=0; i<PointMeshes.Length; i++)
            {
                Rotations[i] += Velocities[i];
            }

            Singularity.Update(gameTime);
        }


        public void Draw(PlayRenderer renderer)
        {
            Matrix rot;
            for (int i=0; i<PointMeshes.Length; i++)
            {
                rot = Matrix.CreateRotationZ(Rotations[i]);
                renderer.DrawStaticMesh(PointMeshes[0], rot);
            }

            renderer.DrawEntity(Singularity);
        }
    }
}