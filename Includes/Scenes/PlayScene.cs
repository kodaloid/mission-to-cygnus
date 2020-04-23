using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTC.Includes.Tweening;
using MTC.Includes.Renderers;
using MTC.Includes.UI;
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
        public OreBank foundOres;             // a bank for the ores we have collected so far in this level.
        private float shipRotateVelocity;     // a decaying value used to simulate lack of friction in space when rotating.
        private Entity veil, sol;             // A square used to fade the screen in and out.
        private Entity clouds, clouds2;       // Two sets of nebula clouds.
        private UI_InventoryMenu invMenu;     // The inventory ui menu.
        private Rectangle dockBounding;       // A bounding box to detect attempts at docking.
        


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
            // the initial rotation velocity.
            shipRotateVelocity = 0;
            // create the fader veil.
            veil = new Entity("square", Vector3.Zero);
            veil.Opacity = 1.0f;
            veil.LocalTransform.Scale = 99999; // make it huge.
            veil.DiffuseColor = Color.Black;
            // a sun.
            sol = new Entity("sol", Vector3.Zero);
            sol.Opacity = 1.0f;
            sol.LocalTransform.Scale = 2;
            sol.DiffuseColor = Color.White;
            // load the cloudset.
            clouds = new Entity("cloud1", Vector3.Zero);
            clouds.LocalTransform.Scale = 2;
            clouds.LocalTransform.Rotation = MathHelper.ToRadians(36.2f);
            clouds.Opacity = 0.9f;
            clouds2 = new Entity("cloud1", Vector3.Zero);
            clouds2.LocalTransform.Scale = 8;
            clouds2.LocalTransform.Rotation = MathHelper.ToRadians(186.2f);
            // load the ui.
            invMenu = new UI_InventoryMenu();
            invMenu.LoadContent(CurrentGame.GraphicsDevice);
            // play the initial state.
            Start(playState);
        }


        public override void Update(GameTime gameTime)
        {
            currentGameTime = gameTime;

            // gather info.
            var ship = CurrentGame.GameState.CurrentLevel.Ship;

            // input.
            switch (playState)
            {
                case PlayState.InventoryMenu:
                    Update_InventoryMenu(gameTime);
                    break;
                case PlayState.Mining:
                    Update_Mining(gameTime);
                    break;
            }

            // global logic.
            tweenPlayer.Update(gameTime);
            stars.Update(gameTime);
            CurrentGame.GameState.Update(gameTime);
            camera.AlignTo(ship);
            invMenu.Update(gameTime);

            // debug.
            CurrentGame.Window.Title = $"Velocity: {ship.Velocity}, Rotation: {ship.LocalTransform.Rotation}, Zoom: {camera.Zoom}";
        }


        public override void Draw(GameTime gameTime)
        {
            var game = CurrentGame;
            var graphicsDevice = game.GraphicsDevice;

            graphicsDevice.SetRenderTarget(game.Canvas);
            graphicsDevice.Clear(Color.Black);

            #region Draw Onto Canvas
            {
                renderer.SetCamera(camera);
                graphicsDevice.BlendState = BlendState.NonPremultiplied;

                // always draw these.
                renderer.DrawEntity(clouds2);
                renderer.DrawEntity(clouds);
                stars.Draw(renderer);
                renderer.DrawEntity(sol);
                CurrentGame.GameState.Draw(renderer);
                renderer.Effect.Alpha = 1;

                #region Draw Any UI
                {
                    game.Batcher.Begin();
                    if (playState == PlayState.InventoryMenu)
                    {
                        invMenu.Draw(game.Batcher);
                        DrawCursor(game.Batcher);
                    }                    
                    game.Batcher.End();
                }
                #endregion

                renderer.DrawEntity(veil);
            }   
            #endregion

            #region Fill Screen With Canvas
            {
                graphicsDevice.SetRenderTarget(null);
                game.Batcher.Begin();
                game.Batcher.Draw(game.Canvas, graphicsDevice.Viewport.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                game.Batcher.End();
            }
            #endregion
        }


        private void DrawCursor(SpriteBatch batcher)
        {
            var game  = CurrentGame;
            var c_tex = game.GetLoadedTexture("cursor");
            var c_vec = GetMouseCursorPosition();
            batcher.Draw(c_tex, c_vec, null, Color.White);
        }


        public Vector2 GetMouseCursorPosition()
        {
            var game  = CurrentGame;
            var c_tex     = game.GetLoadedTexture("cursor");
            var c_pos     = game.Signals.CurrentMouseState.Position;
            var c_scale_x = (float)game.Canvas.Width / (float)game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            var c_scale_y = (float)game.Canvas.Height / (float)game.GraphicsDevice.PresentationParameters.BackBufferHeight;
            return new Vector2(c_pos.X * c_scale_x, c_pos.Y * c_scale_y );
        }


        // --------------------------------------------------------------------------------------------
        // ------ PLAY INITIALIZATION -----------------------------------------------------------------
        // --------------------------------------------------------------------------------------------


        private void Start(PlayState newState)
        {
            playState = newState;
            switch (playState)
            {
                case PlayState.Intro:
                    Start_Intro();
                    break;
                case PlayState.InventoryMenu:
                    Start_InventoryMenu();
                    break;
                case PlayState.Leaving:
                    Start_LeavingTop();
                    break;
                case PlayState.Return:
                    Start_Return();
                    break;
                case PlayState.Mining:
                    Start_Mining();
                    break;
            }
        }


        private void Start_Intro()
        {
            var level = CurrentGame.GameState.CurrentLevel;
            level.SetCaeruleumPosition(-3000, 750);
            level.Ship.Visible = false;

            var startVec   = level.Caeruleum1.WorldTransform.Position;
            var endVec     = new Vector3(15, 750, 0);
            var startTime  = currentGameTime.TotalGameTime;
            var ani1       = tweenPlayer.Add(1.0f, 0.0f, startTime, startTime + TimeSpan.FromSeconds(4));
            var ani2       = tweenPlayer.Add(startVec, endVec, startTime, startTime + TimeSpan.FromSeconds(8));
            var ani3       = tweenPlayer.Add(0.6f, 1.0f, startTime, startTime + TimeSpan.FromSeconds(12));

            // animation mode.
            ani1.Interpolation = TweenInterpolateMode.SmoothStep;
            ani2.Interpolation = TweenInterpolateMode.SmoothStep;
            ani3.Interpolation = TweenInterpolateMode.SmoothStep;

            // handle updates.
            ani1.OnUpdate = delegate(ITween tween1)
            {
                veil.Opacity = (tween1 as FloatTween).CurrentValue;
            };

            ani2.OnUpdate = delegate(ITween tween)
            {
                var v3tween = tween as Vector3Tween;
                level.Caeruleum1.WorldTransform.Position = v3tween.CurrentValue;
                level.Caeruleum2.WorldTransform.Position = v3tween.CurrentValue;
            };

            // handle completion.
            ani2.OnComplete = delegate(ITween tween)
            {
                var v3tween = tween as Vector3Tween;
                level.Caeruleum1.WorldTransform.Position = v3tween.CurrentValue;
                level.Caeruleum2.WorldTransform.Position = v3tween.CurrentValue;
                // create the docking bounding box.
                dockBounding = new Rectangle((int)v3tween.CurrentValue.X - 25, (int)v3tween.CurrentValue.Y - 75, 50, 150);
                Start(PlayState.InventoryMenu);
            };

            // handle updates.
            ani3.OnUpdate = delegate(ITween tween3)
            {
                camera.Zoom = (tween3 as FloatTween).CurrentValue;
            };
        }


        private void Start_InventoryMenu()
        {
            var level = CurrentGame.GameState.CurrentLevel;
        }


        private void Start_LeavingTop()
        {
            var level      = CurrentGame.GameState.CurrentLevel;
            level.Ship.Trajectory = Vector3.Zero;
            shipRotateVelocity = 0;            

            var startVec   = level.Ship.LocalTransform.Position;
            var endVec     = startVec + new Vector3(0, -80, 0);
            var startTime  = currentGameTime.TotalGameTime;
            var ani        = tweenPlayer.Add(startVec, endVec, startTime, startTime + TimeSpan.FromSeconds(2));

            ani.Interpolation = TweenInterpolateMode.SmoothStep;

            ani.OnUpdate = delegate(ITween tween1)
            {
                level.Ship.Visible = true;
                level.Ship.LocalTransform.Position = (tween1 as Vector3Tween).CurrentValue;
            };

            ani.OnComplete = delegate {
                Start(PlayState.Mining);
            };
        }


        private void Start_Return()
        {
            var level      = CurrentGame.GameState.CurrentLevel;
            var startVec   = level.Ship.LocalTransform.Position;
            var endVec     = level.Caeruleum1.LocalTransform.Position + new Vector3(-15, 0, 0);
            var startTime  = currentGameTime.TotalGameTime;
            var ani1       = tweenPlayer.Add(startVec, endVec, startTime, startTime + TimeSpan.FromSeconds(2));
            var curRad     = level.Ship.LocalTransform.Rotation;

            // remove twist.
            while (curRad > MathHelper.TwoPi) curRad -= MathHelper.TwoPi;
            while (curRad < -MathHelper.TwoPi) curRad += MathHelper.TwoPi;

            var ani2       = tweenPlayer.Add(curRad, curRad < MathHelper.Pi ? 0 : MathHelper.TwoPi, startTime, startTime + TimeSpan.FromSeconds(2));
            
            ani1.Interpolation = TweenInterpolateMode.SmoothStep;
            ani2.Interpolation = TweenInterpolateMode.SmoothStep;
            level.Ship.Velocity = 0.0f;

            ani1.OnUpdate = delegate(ITween tween1)
            {
                level.Ship.Visible = true;
                level.Ship.LocalTransform.Position = (tween1 as Vector3Tween).CurrentValue;
            };

            ani2.OnUpdate = delegate(ITween tween2)
            {
                level.Ship.LocalTransform.Rotation = (tween2 as FloatTween).CurrentValue;
            };

            ani1.OnComplete = delegate { 
                invMenu.Alpha = 0;
                Start(PlayState.InventoryMenu);
            };
        }


        private void Start_Mining()
        {
            var level = CurrentGame.GameState.CurrentLevel;
            level.Ship.Visible = true;
        }


        // --------------------------------------------------------------------------------------------
        // ------ UPDATE MECHANISMS -------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------


        private void Update_Mining(GameTime gameTime)
        {
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_CUSTOM_2))
            {
                camera.Zoom += 0.003f;
            }
            else if (CurrentGame.Signals.IsFired(Constants.SIGNAL_CUSTOM_3))
            {
                camera.Zoom -= 0.003f;
            }

            Update_Mining_ShipVelocity();
            Update_Mining_ShipRotation();
            Update_Mining_DockTest();
        }


        private void Update_Mining_ShipRotation()
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


        private void Update_Mining_ShipVelocity()
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


        /// <summary>
        /// Tests to see if the ship is trying to dock with the caeruleum.
        /// </summary>
        private void Update_Mining_DockTest()
        {
            var game = CurrentGame;
            var level = game.GameState.CurrentLevel;
            var ship = level.Ship;
            var pos = ship.LocalTransform.Position;

            if (dockBounding.Contains(pos.X, pos.Y))
            {
                //var midY = dockBounding.Top + (dockBounding.Height / 2);
                Start(PlayState.Return);
            }
        }


        public void Update_InventoryMenu(GameTime gameTime)
        {
            Update_InventoryMenu_Mouse();
        }


        private void Update_InventoryMenu_Mouse()
        {
            if (CurrentGame.Signals.IsFired(Constants.SIGNAL_CUSTOM_4)) // left mouse button.
            {
                Vector2 pos = GetMouseCursorPosition();
                if (invMenu.LaunchButton1.Bounds.Contains(pos))
                {
                    Start(PlayState.Leaving);
                }
            }
        }
    }
}