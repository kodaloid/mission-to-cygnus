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
        private OreBank foundOres;
        private float shipRotateVelocity;


        public PlayScene()
        {
            
        }


        public override void LoadContent()
        {
            camera = new Camera(Vector3.Zero);
            renderer = new PlayRenderer(CurrentGame.GraphicsDevice);

            // load textures.
            CurrentGame.LoadTextureFromFile("ship",       "Assets/ship-sprite1.png");
            CurrentGame.LoadTextureFromFile("caeruleum",  "Assets/caeruleum-main.png");
            CurrentGame.LoadTextureFromFile("caeruleum2", "Assets/caeruleum-main2.png");
            CurrentGame.LoadTextureFromFile("point",      "Assets/star-sprite1.png");
            CurrentGame.LoadTextureFromFile("star",       "Assets/orb-sprite1.png");

            // load stars.
            stars = new StarField(3);
            // load the gamestate.
            state = GameState.Exists ? GameState.Load() : GameState.CreateNew();
            // init the found ores object.
            foundOres = new OreBank();
        }


        public override void Update(GameTime gameTime)
        {
            // gather info.
            var ship = state.CurrentLevel.Ship;

            // input.
            UpdateSignals();
            UpdateShipVelocity();
            UpdateShipRotation();

            // state.
            stars.Update(gameTime);
            state.Update(gameTime);
            camera.AlignTo(ship);

            // debug.
            CurrentGame.Window.Title = $"Velocity: {ship.Velocity}, Rotation: {ship.LocalTransform.Rotation}, Zoom: {camera.Zoom}";
        }


        private void UpdateSignals()
        {
            CurrentGame.Signals.Update();

            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_CUSTOM_2))
            {
                camera.Zoom += 0.003f;
            }
            else if (CurrentGame.Signals.IsFired(Constants.SIGNAL_CUSTOM_3))
            {
                camera.Zoom -= 0.003f;
            }
        }


        private void UpdateShipRotation()
        {
            var ship = state.CurrentLevel.Ship;
            bool rotationChanged = false;
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_RIGHT))
            {
                shipRotateVelocity += 0.0005f;
                shipRotateVelocity = MathHelper.Clamp(shipRotateVelocity, -.05f, .05f);
                rotationChanged = true;
            }
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_LEFT))
            {
                shipRotateVelocity -= 0.0005f;
                shipRotateVelocity = MathHelper.Clamp(shipRotateVelocity, -.05f, .05f);
                rotationChanged = true;
            }

            ship.LocalTransform.Rotation += shipRotateVelocity;
            
            // Slows down crafts rotation when not yawing left/right.
            if (!rotationChanged)
            {
                if (shipRotateVelocity > 0)
                {
                    shipRotateVelocity -= 0.0002f;
                    if (shipRotateVelocity > 0 && shipRotateVelocity < 0.001f) shipRotateVelocity = 0;
                }
                else if (shipRotateVelocity < 0)
                {
                    shipRotateVelocity += 0.0002f;
                    if (shipRotateVelocity < 0 && shipRotateVelocity > -0.001f) shipRotateVelocity = 0;
                }
            }
        }


        private void UpdateShipVelocity()
        {
            var ship = state.CurrentLevel.Ship;
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_UP))
            {
                ship.Velocity += 0.1f;
            }
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_DOWN))
            {
                ship.Velocity -= 0.1f;
            }
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
            state.Draw(renderer);
        }
    }
}