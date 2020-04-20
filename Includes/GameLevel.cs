using Microsoft.Xna.Framework;
using ld46_entry.Includes.Renderers;

namespace ld46_entry.Includes
{
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
            this.Ship = new Entity(null, Vector3.Zero);
            this.Caeruleum1 = new Entity(null, Vector3.Zero);
            this.Caeruleum2 = new Entity(null, Vector3.Zero);
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
    }
}