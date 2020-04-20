namespace MTC.Includes
{
    /// <summary>
    /// Tracks quantities of stored ores.
    /// </summary>
    public sealed class OreBank
    {
        public int Stone { get; set; }
        public int Iron { get; set; }
        public int Nickel { get; set; }
        public int Silicone { get; set; } 
        public int Ice { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }
        public int Uranium { get; set; }


        public OreBank()
        {

        }


        public static OreBank GetDefault()
        {
            return new OreBank()
            {
                Stone = 5000,
                Iron = 5000,
                Nickel = 1000,
                Silicone = 500,
                Ice = 1500
            };
        }


        public override string ToString()
        {
            return $"{Stone}st,{Iron}fe,{Nickel}ni,{Silicone}si,{Ice}ic,{Silver}ag,{Gold}au,{Platinum}pt,{Uranium}u";
        }
    }
}