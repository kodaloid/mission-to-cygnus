using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ld46_entry.Includes.Input
{
    public class SignalSet
    {
        #region Device States


        KeyboardState _oldKeyState;
        MouseState _oldMouseState;
        private readonly GamePadState[] _oldPadStates;
        private readonly GamePadState[] _newPadStates;


        #endregion


        #region Properties


        /// <summary>
        /// Gets or sets a value indicating whether to enable keyboard support.
        /// </summary>
        public bool EnableKeyboard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable mouse support.
        /// </summary>
        public bool EnableMouse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable GamePad #0 support.
        /// </summary>
        public bool EnableGamePad0 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable GamePad #1 support.
        /// </summary>
        public bool EnableGamePad1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable GamePad #2 support.
        /// </summary>
        public bool EnableGamePad2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable GamePad #3 support.
        /// </summary>
        public bool EnableGamePad3 { get; set; }

        /// <summary>
        /// The collection of bound signals.
        /// </summary>
        public List<Signal> Signals { get; private set; }

        /// <summary>
        /// Gets the current state of the keyboard.
        /// </summary>
        /// <value>The state of the current keyboard.</value>
        public KeyboardState CurrentKeyboardState { get; private set; }

        /// <summary>
        /// Gets the current state of the mouse.
        /// </summary>
        public MouseState CurrentMouseState { get; private set; }


        #endregion


        public SignalSet()
        {
            this.Signals = new List<Signal>();
            this._oldPadStates = new GamePadState[4];
            this._newPadStates = new GamePadState[4];
        }


        public void BindMouse(int role, int button, int occurrence = 0)
        {
            Signals.Add(new MouseSignal(occurrence, role, button));
        }


        public void BindGamePad(int role, int button, int deviceIndex, int occurrence = 0)
        {
            Signals.Add(new GamePadSignal(occurrence, role, button, deviceIndex));
        }


        public void Update()
        {
            if (EnableKeyboard)
            {
                _oldKeyState = CurrentKeyboardState;
                CurrentKeyboardState = Keyboard.GetState();
            }

            if (EnableMouse)
            {
                _oldMouseState = CurrentMouseState;
                CurrentMouseState = Mouse.GetState();
            }

            if (EnableGamePad0)
            {
                _oldPadStates[0] = _newPadStates[0];
                _newPadStates[0] = GamePad.GetState(PlayerIndex.One);
            }

            if (EnableGamePad1)
            {
                _oldPadStates[1] = _newPadStates[1];
                _newPadStates[1] = GamePad.GetState(PlayerIndex.Two);
            }

            if (EnableGamePad2)
            {
                _oldPadStates[2] = _newPadStates[2];
                _newPadStates[2] = GamePad.GetState(PlayerIndex.Three);
            }

            if (EnableGamePad3)
            {
                _oldPadStates[3] = _newPadStates[3];
                _newPadStates[3] = GamePad.GetState(PlayerIndex.Four);
            }

            // Update the signals.
            foreach (Signal signal in Signals)
            {
                if (signal is KeyboardSignal)
                {
                    UpdateKeyboardSignal(signal as KeyboardSignal);
                }
                else if (signal is MouseSignal)
                {
                    UpdateMouseSignal(signal as MouseSignal);
                }
                else if (signal is GamePadSignal)
                {
                    UpdateGamePadSignal(signal as GamePadSignal);
                }
            }
        }


        private void UpdateKeyboardSignal(KeyboardSignal signal)
        {
            // TODO: the .ToString() may cause issues.
            if (Enum.TryParse<Keys>(signal.Key.ToString(), out Keys key))
            {
                var oState = _oldKeyState[key];
                var nState = CurrentKeyboardState[key];

                if (signal.Occurrence == Constants.OCCURRENCE_ONCE)
                {
                    signal.IsFired = oState == KeyState.Down && nState == KeyState.Up;
                }
                else if (signal.Occurrence == Constants.OCCURRENCE_ALWAYS)
                {
                    signal.IsFired = oState == KeyState.Down && nState == KeyState.Down;
                }
                else
                {
                    signal.IsFired = false;
                }
            }
        }


        private void UpdateMouseSignal(MouseSignal signal)
        {
            var button = signal.Button;
            ButtonState oState = 0;
            ButtonState nState = 0;

            // Determine which button to poll.
            switch (signal.Button)
            {
                case Constants.MOUSE_LEFT:
                    oState = _oldMouseState.LeftButton;
                    nState = CurrentMouseState.LeftButton;
                    break;
                case Constants.MOUSE_MIDDLE:
                    oState = _oldMouseState.MiddleButton;
                    nState = CurrentMouseState.MiddleButton;
                    break;
                case Constants.MOUSE_RIGHT:
                    oState = _oldMouseState.RightButton;
                    nState = CurrentMouseState.RightButton;
                    break;
                case Constants.MOUSE_XBUTTON1:
                    oState = _oldMouseState.XButton1;
                    nState = CurrentMouseState.XButton1;
                    break;
                case Constants.MOUSE_XBUTTON2:
                    oState = _oldMouseState.XButton2;
                    nState = CurrentMouseState.XButton2;
                    break;
            }

            // Poll the mode.
            if (signal.Occurrence == Constants.OCCURRENCE_ONCE)
            {
                signal.IsFired = oState == ButtonState.Pressed && nState == ButtonState.Released;
            }
            else if (signal.Occurrence == Constants.OCCURRENCE_ALWAYS)
            {
                signal.IsFired = oState == ButtonState.Pressed && nState == ButtonState.Pressed;
            }
            else
            {
                signal.IsFired = false;
            }
        }


        private void UpdateGamePadSignal(GamePadSignal signal)
        {
            var button = signal.Button;
            GamePadState oPadState = _oldPadStates[signal.DeviceIndex];
            GamePadState nPadState = _newPadStates[signal.DeviceIndex];
            ButtonState oState = 0;
            ButtonState nState = 0;

            // Determine which button to poll.
            switch (signal.Button)
            {
                case Constants.GAME_PAD_A:
                    oState = oPadState.Buttons.A;
                    nState = nPadState.Buttons.A;
                    break;
                case Constants.GAME_PAD_B:
                    oState = oPadState.Buttons.B;
                    nState = nPadState.Buttons.B;
                    break;
                case Constants.GAME_PAD_X:
                    oState = oPadState.Buttons.X;
                    nState = nPadState.Buttons.X;
                    break;
                case Constants.GAME_PAD_Y:
                    oState = oPadState.Buttons.Y;
                    nState = nPadState.Buttons.Y;
                    break;
                case Constants.GAME_PAD_BACK:
                    oState = oPadState.Buttons.Back;
                    nState = nPadState.Buttons.Back;
                    break;
                case Constants.GAME_PAD_BIG_BUTTON:
                    oState = oPadState.Buttons.BigButton;
                    nState = nPadState.Buttons.BigButton;
                    break;
                case Constants.GAME_PAD_START:
                    oState = oPadState.Buttons.Start;
                    nState = nPadState.Buttons.Start;
                    break;
                case Constants.GAME_PAD_LEFT_SHOULDER:
                    oState = oPadState.Buttons.LeftShoulder;
                    nState = nPadState.Buttons.LeftShoulder;
                    break;
                case Constants.GAME_PAD_LEFT_STICK:
                    oState = oPadState.Buttons.LeftStick;
                    nState = nPadState.Buttons.LeftStick;
                    break;
                case Constants.GAME_PAD_RIGHT_SHOULDER:
                    oState = oPadState.Buttons.RightShoulder;
                    nState = nPadState.Buttons.RightShoulder;
                    break;
                case Constants.GAME_PAD_RIGHT_STICK:
                    oState = oPadState.Buttons.RightStick;
                    nState = nPadState.Buttons.RightStick;
                    break;
                case Constants.GAME_PAD_D_PAD_UP:
                    oState = oPadState.DPad.Up;
                    nState = nPadState.DPad.Up;
                    break;
                case Constants.GAME_PAD_D_PAD_DOWN:
                    oState = oPadState.DPad.Down;
                    nState = nPadState.DPad.Down;
                    break;
                case Constants.GAME_PAD_D_PAD_LEFT:
                    oState = oPadState.DPad.Left;
                    nState = nPadState.DPad.Left;
                    break;
                case Constants.GAME_PAD_D_PAD_RIGHT:
                    oState = oPadState.DPad.Right;
                    nState = nPadState.DPad.Right;
                    break;
            }

            // Poll the mode.
            if (signal.Occurrence == Constants.OCCURRENCE_ONCE)
            {
                signal.IsFired = oState == ButtonState.Pressed && nState == ButtonState.Released;
            }
            else if (signal.Occurrence == Constants.OCCURRENCE_ALWAYS)
            {
                signal.IsFired = oState == ButtonState.Pressed && nState == ButtonState.Pressed;
            }
            else
            {
                signal.IsFired = false;
            }
        }
    

        /// <summary>
        /// See if a signal has been fired.
        /// </summary>
        public bool IsFired(int command)
        {
            return Signals.Any(t => t.Command == command && t.IsFired);
        }

        /// <summary>
        /// See if two signals have been fired.
        /// </summary>
        public bool IsFired(int command1, int command2)
        {
            return Signals.Any(t => t.Command == command1 && t.IsFired)
                   && Signals.Any(t => t.Command == command2 && t.IsFired);
        }


        public void setEnableKeyboard(bool enable) => EnableKeyboard = enable;


        public void setEnableGamepad(bool enable, int device = 0)
        {
            switch (device)
            {
                case 0: EnableGamePad0 = enable; break;
                case 1: EnableGamePad1 = enable; break;
                case 2: EnableGamePad2 = enable; break;
                case 3: EnableGamePad3 = enable; break;
            }
        }


        public void setKey(int role, int key, int mode = 0)
        {
            Signals.Add(new KeyboardSignal(mode, role, key));
        }


        public void setGamePad(int role, int button, int occurrence = 0, int device = 0)
        {
            Signals.Add(new GamePadSignal(occurrence, role, button, device));
        }
    }
}