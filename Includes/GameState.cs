using System;
using System.IO;
using System.Xml.Serialization;

namespace ld46_entry.Includes
{
    public class GameState
    {
        public GameLevel[] Levels { get; set; }


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


        internal void Save(string xmlFilename)
        {
            XmlSerializer s = new XmlSerializer(typeof(GameState));
            using (FileStream fs = File.OpenWrite(xmlFilename))
            {
                s.Serialize(fs, this);
            }
        }


        internal static GameState Load(string xmlFilename)
        {
            XmlSerializer s = new XmlSerializer(typeof(GameState));
            GameState result = null;
            using (FileStream fs = File.OpenRead(xmlFilename))
            {
                result = s.Deserialize(fs) as GameState;
            }
            return result;
        }
   }
}