using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTC.Includes.UI
{
    /// <summary>
    /// A rectangle...
    /// </summary>
    public class UI_Rectangle : UI_Base
    {
        public Color Background { get; set; }

        public UI_Rectangle() => this.Bounds = Rectangle.Empty;
        public UI_Rectangle(Rectangle bounds) => this.Bounds = bounds;


        public override void Draw(SpriteBatch batcher)
        {
            if (Bounds != Rectangle.Empty)
                batcher.Draw(DefaultTexture, Bounds, null, Background);
        }
    }
}