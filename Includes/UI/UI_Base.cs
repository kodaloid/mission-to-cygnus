using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTC.Includes.UI
{
    /// <summary>
    /// A basic UI element that should be inherited by smarter types.
    /// </summary>
    public abstract class UI_Base
    {
        public Rectangle Bounds { get; set; }

        public UI_Base() => this.Bounds = Rectangle.Empty;
        public UI_Base(Rectangle bounds) => this.Bounds = bounds;


        public virtual void Update(GameTime gameTime)
        {
            
        }


        public abstract void Draw(SpriteBatch batcher);

        protected Texture2D DefaultTexture { get => Game1.CurrentGame.GetLoadedTexture("square"); }
    }
}