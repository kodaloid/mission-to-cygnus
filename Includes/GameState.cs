using Microsoft.Xna.Framework;
using MTC.Includes.Renderers;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MTC.Includes
{
    /// <summary>
    /// Describes the players play state.
    /// </summary>
    public class GameState
    {
        [XmlIgnore]
        private string FileName;


        public GameLevel[] Levels { get; set; }
        public int CurrentLevelIndex { get; set; }
        public GameLevel CurrentLevel { get => Levels[CurrentLevelIndex]; }


        public GameState(string filename) : this()
        {
            FileName = filename;
        }


        public GameState()
        {
            Levels = new GameLevel[] {
                new GameLevel("Orion"),
                new GameLevel("Alpha Void"),
                new GameLevel("Perseus"),
                new GameLevel("Beta Void"),
                new GameLevel("Cygnus")
            };
        }


        public void Update(GameTime gameTime) => CurrentLevel.Update(gameTime);


        public void Draw(PlayRenderer renderer) => CurrentLevel.Draw(renderer);


        internal void Save()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                throw new Exception("GameState was created without a filename!");

            XmlSerializer s = new XmlSerializer(typeof(GameState));
            using (FileStream fs = File.OpenWrite(this.FileName))
            {
                s.Serialize(fs, this);
            }
        }


        internal static GameState Load(string filename)
        {
            XmlSerializer s = new XmlSerializer(typeof(GameState));
            GameState result = null;
            using (FileStream fs = File.OpenRead(filename))
            {
                result = s.Deserialize(fs) as GameState;
            }
            result.FileName = filename;
            return result;
        }
   }
}