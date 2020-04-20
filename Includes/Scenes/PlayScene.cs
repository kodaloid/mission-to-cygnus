using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTC.Includes.Tweening;
using MTC.Includes.Renderers;
using System;

// namespace aliases.

namespace MTC.Includes.Scenes
{
    /// <summary>
    /// Base class for a sprite entity in our game.
    /// </summary>
    public sealed class PlayScene : Scene
    {
        private GameTime currentGameTime;     // a copy of the game time value.
        private TweenPlayer tweenPlayer;      // utility for playing animated tween's.
        private Camera camera;                // utility for managing camera position.
        private PlayRenderer renderer;        // engine for rendering graphics.
        private StarField stars;              // generates the background stars so we don't have to stare at a black void.
        private PlayState playState;          // an enumerable value representing what phase of a level we are in.
        private OreBank foundOres;            // a bank for the ores we have collected so far in this level.
        private float shipRotateVelocity;     // a decaying value used to simulate lack of friction in space when rotating.


        // ------ CONSTRUCTOR -------------------------------------------------------------------------


        public PlayScene(PlayState initialPlayState = 0)
        {
            currentGameTime = new GameTime(TimeSpan.Zero, TimeSpan.Zero);
            playState = initialPlayState;
        }


        // --------------------------------------------------------------------------------------------
        // ------ SCENE METHOD IMPLEMENTATION ---------------------------------------------------------
        // --------------------------------------------------------------------------------------------


        public override void LoadContent()
        {
            // animation.
            tweenPlayer = new TweenPlayer();
            // rendering stuff.
            camera = new Camera(Vector3.Zero);
            renderer = new PlayRenderer(CurrentGame.GraphicsDevice);
            // load stars.
            stars = new StarField(3);
            // init the found ores object.
            foundOres = new OreBank();
            // play the initial state.
            StartPlayState(playState);
        }


        public override void Update(GameTime gameTime)
        {
            currentGameTime = gameTime;
            // gather info.
            var ship = CurrentGame.GameState.CurrentLevel.Ship;

            // input.
            UpdateSignals(gameTime);
            UpdateShipVelocity();
            UpdateShipRotation();

            // state.
            tweenPlayer.Update(gameTime);
            stars.Update(gameTime);
            CurrentGame.GameState.Update(gameTime);
            camera.AlignTo(ship);

            // debug.
            CurrentGame.Window.Title = $"Velocity: {ship.Velocity}, Rotation: {ship.LocalTransform.Rotation}, Zoom: {camera.Zoom}";
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
            CurrentGame.GameState.Draw(renderer);
        }


        // --------------------------------------------------------------------------------------------
        // ------ PLAY INITIALIZATION -----------------------------------------------------------------
        // --------------------------------------------------------------------------------------------


        private void StartPlayState(PlayState newState)
        {
            playState = newState;
            switch (playState)
            {
                case PlayState.Intro:
                    StartPlayState_Intro();
                    break;
                case PlayState.InventoryMenu:
                    StartPlayState_InventoryMenu();
                    break;
                case PlayState.Mining:
                    StartPlayState_Mining();
                    break;
            }
        }


        private void StartPlayState_Intro()
        {
            var level = CurrentGame.GameState.CurrentLevel;
            level.SetCaeruleumPosition(-5000, 750);
            level.Ship.Visible = false;

            var startVec   = level.Caeruleum1.WorldTransform.Position;
            var endVec     = new Vector3(5, 750, 0);
            var startTime  = currentGameTime.TotalGameTime;
            var endTime    = startTime + TimeSpan.FromSeconds(3);
            var animation  = tweenPlayer.Add(startVec, endVec, startTime, endTime);

            // animation mode.
            animation.Interpolation = TweenInterpolateMode.SmoothStep;

            // handle updates.
            animation.OnUpdate = delegate(ITween tween)
            {
                var v3tween = tween as Vector3Tween;
                level.Caeruleum1.WorldTransform.Position = v3tween.CurrentValue;
                level.Caeruleum2.WorldTransform.Position = v3tween.CurrentValue;
            };

            // handle completion.
            animation.OnComplete = delegate(ITween tween)
            {
                var v3tween = tween as Vector3Tween;
                level.Caeruleum1.WorldTransform.Position = v3tween.CurrentValue;
                level.Caeruleum2.WorldTransform.Position = v3tween.CurrentValue;
                StartPlayState(PlayState.Mining);
            };
        }


        private void StartPlayState_InventoryMenu()
        {
            var level = CurrentGame.GameState.CurrentLevel;
        }


        private void StartPlayState_Mining()
        {
            var level = CurrentGame.GameState.CurrentLevel;
            level.Ship.Visible = true;
        }


        // --------------------------------------------------------------------------------------------
        // ------ UPDATE MECHANISMS -------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------


        private void UpdateSignals(GameTime gameTime)
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
            var ship = CurrentGame.GameState.CurrentLevel.Ship;
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
            var ship = CurrentGame.GameState.CurrentLevel.Ship;
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_UP))
            {
                ship.Velocity += 0.1f;
            }
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_MOVE_DOWN))
            {
                ship.Velocity -= 0.1f;
            }
        }
    }
}