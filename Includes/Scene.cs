using Microsoft.Xna.Framework;

namespace ld46_entry.Includes
{
    /// <summary>
    /// Base class for a scene in our game.
    /// </summary>
    public abstract class Scene
    {
        protected Game1 CurrentGame { get => Game1.CurrentGame; }

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}