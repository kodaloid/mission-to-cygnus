using System;
using ld46_entry.Includes.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// namespace aliases.
using C = ld46_entry.Includes.Constants;

namespace ld46_entry.Includes.Scenes
{
    /// <summary>
    /// Base class for a sprite entity in our game.
    /// </summary>
    public sealed class TestPlayScene : Scene
    {
        public Camera camera { get; private set; }
        public PlayRenderer renderer { get; private set; }
        public Entity ship, caeruleum, caeruleum2, singularity;
        float shipRotateVelocity = 0.0f;

        // our star field (3 layers?)
        StaticMesh[] starfieldPointMeshes = new StaticMesh[3];
        float[] starfieldVelocities = new float[3];
        float[] starfieldRotations = new float[3];


        // solar system
        public Entity ourSun, earth, moon;


        public TestPlayScene() { }


        public override void LoadContent()
        {
            var random = new Random();
            var game = this.CurrentGame;

            // create a camera.
            renderer = new PlayRenderer(game.GraphicsDevice);
            camera = new Camera(Vector3.Zero);

            // load ship entity.
            game.LoadTextureFromFile("shipTexture", "Assets/ship-sprite1.png");
            game.LoadTextureFromFile("caeruleumTexture", "Assets/caeruleum-main.png");
            game.LoadTextureFromFile("caeruleumTexture2", "Assets/caeruleum-main2.png");
            game.LoadTextureFromFile("pointTexture", "Assets/star-sprite1.png");
            game.LoadTextureFromFile("starTexture", "Assets/orb-sprite1.png");

            ship = new Entity("shipTexture", new Vector3(0, 750, 0));
            caeruleum = new Entity("caeruleumTexture", new Vector3(0, 750, 0));
            caeruleum2 = new Entity("caeruleumTexture2", new Vector3(0, 750, 0));
            
            // generate a galaxy!
            var galaxyGraph = new GalaxyGraph();

            for (int i=0; i<starfieldPointMeshes.Length; i++) {
                galaxyGraph.GeneratePoints(15000); // always new.
                starfieldPointMeshes[i] = galaxyGraph.ToStaticMesh(game.GraphicsDevice, "pointTexture");
                starfieldVelocities[i] = 0.0002f + (float)(random.NextDouble() * 0.0002);
            }
            
            singularity = new Entity("starTexture", Vector3.Zero);
            singularity.LocalTransform.Scale = 7.5f;
            singularity.DiffuseColor = Color.Black;


            // solar system test.
            ourSun = new Entity("starTexture", new Vector3(0,0,0));
            ourSun.LocalTransform.Scale = 3;
            ourSun.DiffuseColor = Color.Yellow;

            earth = new Entity("starTexture", new Vector3(-420, 0, 0));
            ourSun.Children.Add(earth);

            moon = new Entity("starTexture", new Vector3(0, 50, 0));
            moon.LocalTransform.Scale = 0.2f;
            earth.Children.Add(moon);


            // game state test.
            GameState gs = new GameState();
            GameLevel lv1 = gs.Levels[0];
            lv1.PrimaryBody = ourSun;
            gs.Save("test.xml");
        }


        public override void Update(GameTime gameTime)
        {
            UpdateSignals();

            ourSun.LocalTransform.Rotation += 0.001f;
            earth.LocalTransform.Rotation += 0.002f;

            // make our ship & camera move.
            UpdateShipVelocity();
            UpdateShipRotation();

            ship.Update(gameTime);
            caeruleum.Update(gameTime);

            ourSun.Update(gameTime);
            earth.Update(gameTime);
            moon.Update(gameTime);
            camera.AlignTo(ship);

            // make our galaxy move!
            starfieldRotations[0] += starfieldVelocities[0];
            starfieldRotations[1] += starfieldVelocities[1];
            starfieldRotations[2] += starfieldVelocities[2];
            CurrentGame.Window.Title = $"Velocity: {ship.Velocity}, Rotation: {ship.LocalTransform.Rotation}, Zoom: {camera.Zoom}";
        }


        private void UpdateSignals()
        {
            CurrentGame.Signals.Update();

            if (CurrentGame.Signals.IsFired(C.SIGNAL_CUSTOM_2))
            {
                camera.Zoom += 0.003f;
            }
            else if (CurrentGame.Signals.IsFired(C.SIGNAL_CUSTOM_3))
            {
                camera.Zoom -= 0.003f;
            }
        }


        private void UpdateShipRotation()
        {
            bool rotationChanged = false;
            if (CurrentGame.Signals.IsFired(C.SIGNAL_MOVE_RIGHT))
            {
                shipRotateVelocity += 0.0005f;
                shipRotateVelocity = MathHelper.Clamp(shipRotateVelocity, -.05f, .05f);
                rotationChanged = true;
            }
            if (CurrentGame.Signals.IsFired(C.SIGNAL_MOVE_LEFT))
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
            // bool velocityChanged = false;
            if (CurrentGame.Signals.IsFired(C.SIGNAL_MOVE_UP))
            {
                ship.Velocity += 0.1f;
                //velocityChanged = true;
            }
            if (CurrentGame.Signals.IsFired(C.SIGNAL_MOVE_DOWN))
            {
                ship.Velocity -= 0.1f;
                //velocityChanged = true;
            }

            // Slows down craft movement when not accelerating.
           /* if (!velocityChanged)
            {
                if (ship.Velocity > 0)
                {
                    ship.Velocity -= 0.02f;
                    if (ship.Velocity > 0 && ship.Velocity < 0.02f) ship.Velocity = 0;
                }
                else if (ship.Velocity < 0)
                {
                    ship.Velocity += 0.02f;
                    if (ship.Velocity < 0 && ship.Velocity > -0.02f) ship.Velocity = 0;
                }
            } */
        }


        public override void Draw(GameTime gameTime)
        {
            var graphicsDevice = CurrentGame.GraphicsDevice;
            renderer.SetCamera(camera);

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            renderer.DrawStaticMesh(starfieldPointMeshes[0], Matrix.CreateRotationZ(starfieldRotations[0]));
            renderer.DrawStaticMesh(starfieldPointMeshes[1], Matrix.CreateRotationZ(starfieldRotations[1]));
            renderer.DrawStaticMesh(starfieldPointMeshes[2], Matrix.CreateRotationZ(starfieldRotations[2]));
            renderer.DrawEntity(singularity);
            renderer.DrawEntity(ourSun);
            renderer.DrawEntity(caeruleum);
            renderer.DrawEntity(ship);
            renderer.DrawEntity(caeruleum2);
        }
    }
}