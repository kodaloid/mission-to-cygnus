using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTC.Includes.Renderers;
using System.IO;

// namespace aliases.

namespace MTC.Includes.Scenes
{
    /// <summary>
    /// Base class for a sprite entity in our game.
    /// </summary>
    public sealed class PlayScene : Scene
    {
        private Camera camera;
        private PlayRenderer renderer;
        private StarField stars;
        private GameState state;


        public PlayScene()
        {
            string file = "Data/SaveState.mtc";
            if (File.Exists(file))
            {
                state = GameState.Load(file);
            }
            else
            {
                state = new GameState(file);
                state.Save();
            }
        }


        public override void LoadContent()
        {
            camera = new Camera(Vector3.Zero);
            renderer = new PlayRenderer(CurrentGame.GraphicsDevice);

            // load textures.
            CurrentGame.LoadTextureFromFile("shipTexture", "Assets/ship-sprite1.png");
            CurrentGame.LoadTextureFromFile("caeruleumTexture", "Assets/caeruleum-main.png");
            CurrentGame.LoadTextureFromFile("caeruleumTexture2", "Assets/caeruleum-main2.png");
            CurrentGame.LoadTextureFromFile("pointTexture", "Assets/star-sprite1.png");
            CurrentGame.LoadTextureFromFile("starTexture", "Assets/orb-sprite1.png");

            // load states.
            stars = new StarField(3);
        }


        public override void Update(GameTime gameTime)
        {
            stars.Update(gameTime);
            //state.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var graphicsDevice = CurrentGame.GraphicsDevice;
            renderer.SetCamera(camera);

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            stars.Draw(renderer);
            //state.Draw(renderer);
        }
    }
}