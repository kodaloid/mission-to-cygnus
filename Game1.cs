﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MTC.Includes;
using MTC.Includes.Input;
using MTC.Includes.Scenes;
using System.Collections.Generic;
using System.IO;

// namespace aliases.
using C = MTC.Includes.Constants;

namespace MTC
{
    public class Game1 : Game
    {
        // very ugly stuff here, please burn with fire.
        private static Game1 currentGame;
        private GraphicsDeviceManager graphics;


        public static Game1 CurrentGame { get { return currentGame; } }
        public SignalSet Signals { get; private set; }
        public SceneCollection Scenes { get; private set; }
        public Dictionary<string, Texture2D> Textures { get; private set; }


        public Game1()
        {
            currentGame = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            Window.AllowUserResizing = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }


        ~Game1() => currentGame = null;


        protected override void Initialize() => base.Initialize();


        /// <summary>
        /// Load any content (in our case the first scene the user sees).
        /// </summary>
        protected override void LoadContent()
        {
            // set-up our input.
            LoadControlScheme();

            // create a dictionary for our manually loaded textures.
            Textures = new Dictionary<string, Texture2D>();

            // init our scene.
            var scene = new PlayScene();
            scene.LoadContent();
            Scenes = new SceneCollection();
            Scenes.Show(scene);

        }


        /// <summary>
        /// Update the currently active scene.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (Signals.IsFired(C.SIGNAL_ESCAPE))
            {
                CurrentGame.Exit();
            }

            Scenes.Current?.Update(gameTime);
            base.Update(gameTime);
        }        


        /// <summary>
        /// Draw the currently active scene.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Scenes.Current?.Draw(gameTime);

            base.Draw(gameTime);
        }


        /// <summary>
        /// Load/set-up any user input bindings the game will have.
        /// </summary>
        private void LoadControlScheme()
        {
            // our input system.
            Signals = new SignalSet();
            Signals.EnableKeyboard = true;

            // control bindings.
            Signals.setKey(C.SIGNAL_MOVE_LEFT, C.KEYS_A, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_RIGHT, C.KEYS_D, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_UP, C.KEYS_W, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_DOWN, C.KEYS_S, C.OCCURRENCE_ALWAYS);
            // arrows
            Signals.setKey(C.SIGNAL_MOVE_LEFT, C.KEYS_LEFT, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_RIGHT, C.KEYS_RIGHT, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_UP, C.KEYS_UP, C.OCCURRENCE_ALWAYS);
            Signals.setKey(C.SIGNAL_MOVE_DOWN, C.KEYS_DOWN, C.OCCURRENCE_ALWAYS);
            // other
            Signals.setKey(C.SIGNAL_ESCAPE, C.KEYS_ESCAPE, C.OCCURRENCE_ONCE); // exit
            Signals.setKey(C.SIGNAL_CUSTOM_2, C.KEYS_P, C.OCCURRENCE_ALWAYS); // zoom out
            Signals.setKey(C.SIGNAL_CUSTOM_3, C.KEYS_O, C.OCCURRENCE_ALWAYS); // zoom in
        }


        /// <summary>
        /// Load a texture from a file and give it a name.
        /// </summary>
        public void LoadTextureFromFile(string name, string filename)
        {
            if (!this.Textures.ContainsKey(name))
            {
                Texture2D tex = TextureFromFile(this.GraphicsDevice, filename);
                this.Textures.Add(name, tex);
            }
        }


        /// <summary>
        /// Load a texture from a file.
        /// </summary>
        private Texture2D TextureFromFile(GraphicsDevice device, string filename)
        {
            Texture2D newTexture = null;
            using (FileStream fs = File.OpenRead(filename))
            {
                newTexture = Texture2D.FromStream(device, fs);
            }
            return newTexture;
        }    
    }
}