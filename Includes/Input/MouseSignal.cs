namespace MTC.Includes.Input
{
    public sealed class MouseSignal : Signal
    {
        public int Button { get; set; }


        public MouseSignal(int occurrence, int command, int button)
        {
            this.Device = Constants.SIGNAL_DEVICE_MOUSE;
            this.Occurrence = occurrence;
            this.Command = command;
            this.Button = button;
        }
    }
}