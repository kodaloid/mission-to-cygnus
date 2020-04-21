using Microsoft.Xna.Framework;
using MTC.Includes.Renderers;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        
            for (int i=0; i<layerCount; i++) {
                Thread.Sleep(50);
                var galaxyGraph = new GalaxyGraph();
                galaxyGraph.GeneratePoints(15000); // always new.
                PointMeshes[i] = galaxyGraph.ToStaticMesh(game.GraphicsDevice, "point");
                Velocities[i] = 0.0002f + (float)(random.NextDouble() * 0.0002);
                Rotations[i] = (float)(random.NextDouble() * MathHelper.ToRadians(90));
            }

            Singularity = new Entity("star", Vector3.Zero);
            Singularity.LocalTransform.Scale = 16.5f;
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