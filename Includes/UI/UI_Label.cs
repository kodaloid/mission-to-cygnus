using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTC.Includes.UI
{
    /// <summary>
    /// A rectangle...
    /// </summary>
    public class UI_Label : UI_Base
    {
        private string text;

        public SpriteFont Font { get; }
        public string Text { get => text; set { text = value; CalculateBounds(); } }
        public Color Foreground { get; set; }
        public Color Background { get; set; }

        public UI_Label(SpriteFont font, int x, int y, string text, Color color)
        {
            Font = font;
            Text = text;
            Foreground = color;
            Bounds = new Rectangle(new Point(x, y), Point.Zero);
            CalculateBounds();
        }


        public void CalculateBounds()
        {
            var m = Font.MeasureString(Text);
            int w = (int)m.X;
            int h = (int)m.Y;
            Bounds = new Rectangle(Bounds.X, Bounds.Y, w, h);
        }


        public override void Draw(SpriteBatch batcher)
        {
            batcher.DrawString(Font, Text, Bounds.Location.ToVector2(), Foreground);
        }
    }
}