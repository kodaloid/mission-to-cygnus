using System;
using Microsoft.Xna.Framework;

namespace ld46_entry.Includes
{
    /// <summary>
    /// Class that specifies how to generate a GalaxyGraph.
    /// </summary>
    public class GalaxyOptions
    {
        private static Random __rand = new Random();
        private static Random _random { get { return __rand; } }
        
        public float ArmCount = 3;
        public float ArmSpread = 0.2f;
        public float Gravity = 20;
        public float ConformChance = 0.7f;
        public float ScalingAngleStart = 0.2f;
        public float ScalingAngleEnd = 0.5f;
        public float ScalingPower = 0.1f;
        public float GalaxySize = 650000.0f;
        public float RotationStrength = 0.666f;
        
        public GalaxyOptions() { }
    }
}