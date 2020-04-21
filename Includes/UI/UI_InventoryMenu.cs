using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTC.Includes.Scenes;

namespace MTC.Includes.UI
{
    /// <summary>
    /// A basic UI element that should be inherited by smarter types.
    /// </summary>
    public class UI_InventoryMenu : UI_Rectangle
    {
        private TimeSpan updateInterval;

        public UI_Rectangle LaunchButton1;
        public UI_Label[] LabelColumn1, LabelColumn2, LabelColumn3;
        public int Alpha;


        public UI_InventoryMenu()
        {
            
        }


        public void LoadContent(GraphicsDevice device)
        {
            var game = Game1.CurrentGame;

            this.Background = new Color(Color.Gray, 190);
            Reposition(device);

            this.LaunchButton1 = new UI_Rectangle(new Rectangle(128 + Bounds.Left, 270 + Bounds.Top, 77, 31));
            this.LaunchButton1.Background = Color.Transparent;

            LabelColumn1 = new UI_Label[9];
            LabelColumn2 = new UI_Label[9];
            LabelColumn3 = new UI_Label[9];
            for (int i=0, y=0; i<9; i++, y+=20) {
                LabelColumn1[i] = new UI_Label(game.OreInventoryFont, 33 + Bounds.Left, 50 + Bounds.Top + y, "st: 0", Color.White);
                LabelColumn2[i] = new UI_Label(game.OreInventoryFont, 195 + Bounds.Left, 50 + Bounds.Top + y, "st: 0", Color.White);
                LabelColumn3[i] = new UI_Label(game.OreInventoryFont, 355 + Bounds.Left, 50 + Bounds.Top + y, "st: 0", Color.White);
            }

            Alpha = 0;
        }


        public void Reposition(GraphicsDevice device)
        {
            var viewport   = device.Viewport.Bounds;
            int menuWidth  = 500;
            int menuHeight = 320;
            int menuX      = (viewport.Width / 2) - 250;
            int menuY      = (viewport.Height / 2) - 160;
            this.Bounds    = new Rectangle(menuX, menuY, menuWidth, menuHeight);
        }


        public override void Update(GameTime gameTime)
        {
            updateInterval += gameTime.ElapsedGameTime;
            if (updateInterval.TotalSeconds < 5) return;

            var st = Game1.CurrentGame.GameState;
            var ps = Game1.CurrentGame.Scenes.Current as PlayScene;

            LabelColumn1[0].Text = $"st: {ps.foundOres.Stone}";
            LabelColumn1[1].Text = $"fe: {ps.foundOres.Iron}";
            LabelColumn1[2].Text = $"ni: {ps.foundOres.Nickel}";
            LabelColumn1[3].Text = $"si: {ps.foundOres.Silicone}";
            LabelColumn1[4].Text = $"ic: {ps.foundOres.Ice}";
            LabelColumn1[5].Text = $"ag: {ps.foundOres.Silver}";
            LabelColumn1[6].Text = $"au: {ps.foundOres.Gold}";
            LabelColumn1[7].Text = $"pt: {ps.foundOres.Platinum}";
            LabelColumn1[8].Text = $"ur: {ps.foundOres.Uranium}";

            LabelColumn2[0].Text = $"st: {st.CollectedOres.Stone}";
            LabelColumn2[1].Text = $"fe: {st.CollectedOres.Iron}";
            LabelColumn2[2].Text = $"ni: {st.CollectedOres.Nickel}";
            LabelColumn2[3].Text = $"si: {st.CollectedOres.Silicone}";
            LabelColumn2[4].Text = $"ic: {st.CollectedOres.Ice}";
            LabelColumn2[5].Text = $"ag: {st.CollectedOres.Silver}";
            LabelColumn2[6].Text = $"au: {st.CollectedOres.Gold}";
            LabelColumn2[7].Text = $"pt: {st.CollectedOres.Platinum}";
            LabelColumn2[8].Text = $"ur: {st.CollectedOres.Uranium}";

            LabelColumn3[0].Text = $"st: {st.BankedOres.Stone}";
            LabelColumn3[1].Text = $"fe: {st.BankedOres.Iron}";
            LabelColumn3[2].Text = $"ni: {st.BankedOres.Nickel}";
            LabelColumn3[3].Text = $"si: {st.BankedOres.Silicone}";
            LabelColumn3[4].Text = $"ic: {st.BankedOres.Ice}";
            LabelColumn3[5].Text = $"ag: {st.BankedOres.Silver}";
            LabelColumn3[6].Text = $"au: {st.BankedOres.Gold}";
            LabelColumn3[7].Text = $"pt: {st.BankedOres.Platinum}";
            LabelColumn3[8].Text = $"ur: {st.BankedOres.Uranium}";

            if (Alpha < 255) Alpha++;
        }


        public override void Draw(SpriteBatch batcher)
        {
            if (Bounds != Rectangle.Empty)
            {
                var tex = Game1.CurrentGame.GetLoadedTexture("inv-menu");
                batcher.Draw(tex, Bounds, null, Background);
                LaunchButton1.Draw(batcher);
                for (int i=0; i<9; i++)
                {
                    LabelColumn1[i].Draw(batcher);
                    LabelColumn2[i].Draw(batcher);
                    LabelColumn3[i].Draw(batcher);
                }
            }
        }
    }
}