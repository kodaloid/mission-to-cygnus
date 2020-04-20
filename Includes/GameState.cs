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
        public static bool Exists { get => File.Exists(Constants.STATE_FILE); }


        public GameLevel[] Levels { get; set; }
        public int CurrentLevelIndex { get; set; }
        public GameLevel CurrentLevel { get => Levels[CurrentLevelIndex]; }
        public OreBank CollectedOres { get; set; }
        public OreBank BankedOres { get; set; }


        public GameState() { }


        public static GameState CreateNew()
        {
            return new GameState()
            {
                Levels = new GameLevel[] {
                    new GameLevel("Orion"),
                    new GameLevel("Alpha Void"),
                    new GameLevel("Perseus"),
                    new GameLevel("Beta Void"),
                    new GameLevel("Cygnus")
                },
                CollectedOres = OreBank.GetDefault(),
                BankedOres = new OreBank(),
            };
        }


        public void Update(GameTime gameTime)
        {
             CurrentLevel.Update(gameTime);
        }


        public void Draw(PlayRenderer renderer)
        {
            CurrentLevel.Draw(renderer);
        }


        internal void Save()
        {
            if (GameState.Exists)
            {
                File.Delete(Constants.STATE_FILE);
            }

            XmlSerializer s = new XmlSerializer(typeof(GameState));
            using (FileStream fs = File.OpenWrite(Constants.STATE_FILE))
            {
                s.Serialize(fs, this);
            }
        }


        internal static GameState Load()
        {
            XmlSerializer s = new XmlSerializer(typeof(GameState));
            GameState result = null;
            using (FileStream fs = File.OpenRead(Constants.STATE_FILE))
            {
                result = s.Deserialize(fs) as GameState;
            }
            return result;
        }
   }
}