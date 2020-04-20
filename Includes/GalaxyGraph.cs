using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld46_entry.Includes
{
    /// <summary>
    /// Class that generates a galaxy to explore!
    /// </summary>
    public class GalaxyGraph
    {
        private Random _random;
        private Color[] _colors;
        public GalaxyOptions Options { get; private set; }
        public List<GalaxyPoint> Points { get; private set; }


        public GalaxyGraph(GalaxyOptions options = null)
        {
            this._random = new Random();
            this._colors = new Color[] {
                RandomColor(),
                RandomColor(),
                RandomColor(),
                RandomColor()
            };
            this.Options = options == null ? new GalaxyOptions() : options;
            this.Points = new List<GalaxyPoint>();
        }


        public void GeneratePoints(int max)
        {
            this.Points.Clear();

            var c1 = _colors[0];
            var c2 = _colors[1];
            var c3 = _colors[2];
            var c4 = _colors[3];

            // maxMagnitude stores the distance of the furthest known point
            float maxMagnitude = 1.0f;

            for (int i=0; i<max; i++)
            {
                Vector3 v = GetPoint();
                Color c = Color.White;

                // Colour each point based on their global angle around the axis
                double a = Math.Atan2(v.X, v.Z);

                // c1, c2, c3 and c4 are random colours generated at the start of the function
                // Blend the colours of adjacent quadrants together
                if (a < -MathHelper.Pi / 2f)
                {
                    c = Color.Lerp(c1, c2, (float)(a + MathHelper.Pi) / MathHelper.PiOver2);
                }
                else if (a < 0)
                {
                    c = Color.Lerp(c2, c3, (float)(MathHelper.Pi / 2 + a) / MathHelper.PiOver2);
                }
                else if (a < MathHelper.Pi / 2f)
                {
                    c = Color.Lerp(c3, c4, (float)(a / MathHelper.PiOver2));
                }
                else
                {
                    c = Color.Lerp(c4, c1, (float)((a - MathHelper.PiOver2) / MathHelper.PiOver2));
                }

                if (v.Length() / maxMagnitude > 1)
                {
                    maxMagnitude = v.Length();
                }

                // Colours get brighter the closer they are to the center
                float magnitude = v.Length();
                float lerp = magnitude / maxMagnitude;

                c = Color.Lerp(Color.White, c, 0.5f + MathHelper.Min(1, lerp));
                //c = Color.Lerp(Color.Black, Color.Lerp(Color.White, c, MathHelper.Min(1, lerp)), 0.8f);

                // add the point.
                this.Points.Add(new GalaxyPoint(v, c));
            }
        }


        public void GenerateStars(int max)
        {
            this.Points.Clear();

            for (int i=0; i<max; i++)
            {
                Vector3 v = GetPoint();
                //v += NextV3(0.0f, 0.7f);
                v = new Vector3(v.X * 20, v.Y * 20, 1);

                // Brighten stars slightly.
                var c = Color.Lerp(_colors[0], Color.White, (float)NextDouble(0, 0.5));

                // add the star.
                this.Points.Add(new GalaxyPoint(v, c));
            }
        }


        private Vector3 GetPoint()
        {
            Vector3 vector;
            double armDivisor = Math.PI / Options.ArmCount;

            while (true)
            {
                vector = NextV3(-1, 1);
                vector.Normalize();
                vector *= (float)Math.Pow(NextDouble(0, 1.0), Options.Gravity);

                if (_random.NextDouble() > Options.ConformChance) break;

                bool valid = false;

                var d = Math.Atan2(vector.X, vector.Y);

                for (double j = -Math.PI; j <= Math.PI; j += armDivisor)
                {
                    if (d > j && d < j + armDivisor * Options.ArmSpread)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid) break;
            }

            // Calculate the global angle of the point around the axis
            var a = Math.Atan2(vector.X, vector.Y);

            // Calculate the angle of this point inside its quadrant
            var e = Math.Abs(Math.Abs(a) - Math.PI / 2);

            var magnitude = Options.GalaxySize;

            // Scale the point outwards if it lands within the scaling region
            if (e > Options.ScalingAngleStart && e < Options.ScalingAngleEnd)
                magnitude = magnitude * 2.0f / (float)Math.Pow(e, Options.ScalingPower);

            vector *= new Vector3(vector.X * magnitude, vector.Y * magnitude, vector.Z);

            // Rotate the point around the galaxy depending on its magnitude
            vector = Vector3.Transform(vector, Matrix.CreateRotationZ( (float)(-vector.Length() * Options.RotationStrength) ) );

            return vector;
        }


        private double NextDouble(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }


        private Vector3 NextV3(double min, double max)
        {
            return new Vector3(
                (float)(_random.NextDouble() * (max - min) + min),
                (float)(_random.NextDouble() * (max - min) + min),
                0
            );
        }
    

        private Color RandomColor()
        {
            return new Color((byte)_random.Next(0, 255), (byte)_random.Next(0, 255), (byte)_random.Next(0, 255));
        }
    

        public StaticMesh ToStaticMesh(GraphicsDevice device, string texture, float scaleMin = 0.05f, float scaleMax = 0.2f)
        {
            var tex2d = Game1.CurrentGame.Textures[texture];
            var builder = new StaticMeshBuilder();
            float scale;

            foreach (var point in this.Points)
            {
                scale = (float)NextDouble(scaleMin, scaleMax);
                builder.AddQuad(point.Position, tex2d.Width, tex2d.Height, point.Color, scale);
            }

            return builder.ToMesh(tex2d);
        }
    }
}