namespace MTC.Includes.Input
{
    public abstract class Signal
    {
        public int Device { get; set; }

        public int Occurrence { get; set; }

        public int Command { get; set; }

        public bool IsFired { get; set; }


        public override string ToString()
        {
            return string.Format("[{0}] ({1}) {2}", Device, Occurrence, Command);
        }
    }
}