using System;
using Microsoft.Xna.Framework;
using MTC.Includes.Renderers;

namespace MTC.Includes
{
    /// <summary>
    /// Describes a level in the GameState.
    /// </summary>
    public class GameLevel
    {
        public string Name { get; set; }
        public Entity[] PrimaryBody { get; set; }
        public float[] PrimaryBodySpeeds { get; set; }
        public float[] PrimaryBodySubSpeeds { get; set; }
        public Entity Ship { get; set; }
        public Entity Caeruleum1 { get; set; }
        public Entity Caeruleum2 { get; set; }

        
        public GameLevel() : this("Untitled") { }


        public GameLevel(string name)
        {
            this.Name = name;
            this.PrimaryBody = new Entity[0];
            this.Ship =  new Entity("ship", new Vector3(0, 750, 0));
            this.Ship.Visible = false;
            this.Caeruleum1 = new Entity("caeruleum", new Vector3(-5000, 750, 0));
            this.Caeruleum2 = new Entity("caeruleum2", new Vector3(-5000, 750, 0));

            // TODO: test.
            Initialize(300, new float[0], false);
        }


        // Generate the entities for this level before it begins getting
        // played. Complexity represents what fraction bodies to generate per
        // complexity layer, for example a level could have 500 bodies, with a
        // complexity graph of .3:.7 or .2:.6:.2 for 3 layers.
        public void Initialize(int bodyCount, float[] complexity, bool isVoid)
        {
            Random r = new Random();
            this.PrimaryBody = new Entity[10];
            this.PrimaryBodySpeeds = new float[10];
            this.PrimaryBodySubSpeeds = new float[10];

            for (int i=0; i<10; i++)
            {
                Vector3 direction = Vector3.Transform(new Vector3(0,-1,0), Matrix.CreateRotationZ((float)r.NextDouble() * MathHelper.TwoPi));
                direction.Normalize();
                float magnitude = 5000 + (float)(r.NextDouble() * 10000);
                Entity planet = new Entity("sol", direction * magnitude);
                planet.LocalTransform.Scale = 1f + (float)(r.NextDouble() * 4.0);

                if (magnitude > 6000)
                {
                    planet.LocalTransform.Scale = 1f + (float)(r.NextDouble() * 8.0);
                }
                if (magnitude > 8000)
                {
                    planet.LocalTransform.Scale = 1f + (float)(r.NextDouble() * 16.0);
                }

                this.PrimaryBody[i] = new Entity(null, Vector3.Zero);
                this.PrimaryBody[i].Children.Add(planet);
                this.PrimaryBodySpeeds[i] = 0.0005f + (float)(r.NextDouble() * 0.0008);
                this.PrimaryBodySubSpeeds[i] = 0.0001f + (float)(r.NextDouble() * 0.0004);

                var chance = r.NextDouble();
                if (chance > 0.5f)
                {
                    Vector3 satDir = Vector3.Transform(new Vector3(0,-1,0), Matrix.CreateRotationZ((float)r.NextDouble() * MathHelper.TwoPi));
                    satDir.Normalize();
                    float satMag = 100 + (float)(r.NextDouble() * 300);
                    planet.Children.Add(new Entity("star", direction * satMag));
                }
            }
            
            // TODO: generator code!
        }


        public void Update(GameTime gameTime)
        {
            for (int i=0; i<PrimaryBody.Length; i++)
            {
                PrimaryBody[i].Update(gameTime);
                PrimaryBody[i].LocalTransform.Rotation += this.PrimaryBodySpeeds[i];
                foreach (var body in PrimaryBody[i].Children)
                {
                    body.LocalTransform.Rotation += this.PrimaryBodySubSpeeds[i];
                }
            }
            
            Ship.Update(gameTime);
            Caeruleum1.Update(gameTime);
            Caeruleum2.Update(gameTime);
        }


        public void Draw(PlayRenderer renderer)
        {
            for (int i=0; i<PrimaryBody.Length; i++)
            {
                renderer.DrawEntity(PrimaryBody[i]);
            }            
            renderer.DrawEntity(Caeruleum1);
            renderer.DrawEntity(Ship);

            Caeruleum2.Texture = Game1.CurrentGame.AltFrame ? "caeruleum2a" : "caeruleum2b";

            renderer.DrawEntity(Caeruleum2);
        }


        public void SetCaeruleumPosition(float x, float y) 
            => SetCaeruleumPosition(new Vector3(x, y, 0));


        public void SetCaeruleumPosition(Vector3 position)
        {
            Caeruleum1.WorldTransform.Position = position;
            Caeruleum2.WorldTransform.Position = position;
        }
    }
}