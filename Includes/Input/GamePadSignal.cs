namespace MTC.Includes.Input
{
    public class GamePadSignal : Signal
    {
        public int DeviceIndex { get; }
        public int Button { get; set; }


        public GamePadSignal(int occurrence, int command, int button, int deviceIndex)
        {
            this.Device = Constants.SIGNAL_DEVICE_GAMEPAD;
            this.DeviceIndex = deviceIndex;
            this.Occurrence = occurrence;
            this.Command = command;
            this.Button = button;
        }
    }
}