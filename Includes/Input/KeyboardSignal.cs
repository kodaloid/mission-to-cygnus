namespace MTC.Includes.Input
{
    public sealed class KeyboardSignal : Signal
    {
        public int Key { get; set; }


        public KeyboardSignal(int mode, int command, int key)
        {
            this.Device = Constants.SIGNAL_DEVICE_KEYBOARD;
            this.Occurrence = mode;
            this.Command = command;
            this.Key = key;
        }
    }
}