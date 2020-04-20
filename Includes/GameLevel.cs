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
        public Entity PrimaryBody { get; set; }
        public Entity Ship { get; set; }
        public Entity Caeruleum1 { get; set; }
        public Entity Caeruleum2 { get; set; }

        
        public GameLevel() : this("Untitled") { }


        public GameLevel(string name)
        {
            this.Name = name;
            this.PrimaryBody = new Entity(null, Vector3.Zero);
            this.Ship =  new Entity("ship", new Vector3(0, 750, 0));
            this.Ship.Visible = false;
            this.Caeruleum1 = new Entity("caeruleum", new Vector3(-5000, 750, 0));
            this.Caeruleum2 = new Entity("caeruleum2", new Vector3(-5000, 750, 0));
        }


        // Generate the entities for this level before it begins getting
        // played. Complexity represents what fraction bodies to generate per
        // complexity layer, for example a level could have 500 bodies, with a
        // complexity graph of .3:.7 or .2:.6:.2 for 3 layers.
        public void Initialize(int bodyCount, float[] complexity, bool isVoid)
        {
            // TODO: generator code!
        }


        public void Update(GameTime gameTime)
        {
            PrimaryBody.Update(gameTime);
            Ship.Update(gameTime);
            Caeruleum1.Update(gameTime);
            Caeruleum2.Update(gameTime);
        }


        public void Draw(PlayRenderer renderer)
        {
            renderer.DrawEntity(PrimaryBody);
            renderer.DrawEntity(Caeruleum1);
            renderer.DrawEntity(Ship);
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