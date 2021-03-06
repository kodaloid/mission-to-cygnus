
namespace MTC.Includes
{
    /// <summary>
    /// Holds a number of global constant values used mostly by the input engine.
    /// </summary>
    public abstract class Constants
    {
        public const string STATE_FILE = "Data/SaveState.mtc";

        // -- signal commands --------------------------------------------------------
        public const int SIGNAL_UNKNOWN = 0;
        public const int SIGNAL_ESCAPE = 1000;
        public const int SIGNAL_START = 1001;
        public const int SIGNAL_PRIMARY = 1002;
        public const int SIGNAL_SECONDARY = 1003;
        public const int SIGNAL_MOVE_UP = 1004;
        public const int SIGNAL_MOVE_DOWN = 1005;
        public const int SIGNAL_MOVE_LEFT = 1006;
        public const int SIGNAL_MOVE_RIGHT = 1007;
        public const int SIGNAL_CUSTOM_1 = 1100;
        public const int SIGNAL_CUSTOM_2 = 1101;
        public const int SIGNAL_CUSTOM_3 = 1102;
        public const int SIGNAL_CUSTOM_4 = 1103;
        public const int SIGNAL_CUSTOM_5 = 1104;
        public const int SIGNAL_CUSTOM_6 = 1105;
        public const int SIGNAL_CUSTOM_7 = 1106;
        public const int SIGNAL_CUSTOM_8 = 1107;
        public const int SIGNAL_CUSTOM_9 = 1108;

        // -- occurrence -------------------------------------------------------------
        public const int OCCURRENCE_ALWAYS = 0;
        public const int OCCURRENCE_ONCE = 1;
        public const int OCCURRENCE_TWICE = 2;

        // input devices -------------------------------------------------------------
        public const int SIGNAL_DEVICE_KEYBOARD = 0;
        public const int SIGNAL_DEVICE_MOUSE = 1;
        public const int SIGNAL_DEVICE_GAMEPAD = 2;

        //-- Keyboard Keys -----------------------------------------------------------
        public const int KEYS_NONE = 0;
        public const int KEYS_BACK = 8;
        public const int KEYS_TAB = 9;
        public const int KEYS_ENTER = 13;
        public const int KEYS_PAUSE = 19;
        public const int KEYS_CAPSLOCK = 20;
        public const int KEYS_KANA = 21;
        public const int KEYS_KANJI = 25;
        public const int KEYS_ESCAPE = 27;
        public const int KEYS_IMECONVERT = 28;
        public const int KEYS_IMENOCONVERT = 29;
        public const int KEYS_SPACE = 32;
        public const int KEYS_PAGEUP = 33;
        public const int KEYS_PAGEDOWN = 34;
        public const int KEYS_END = 35;
        public const int KEYS_HOME = 36;
        public const int KEYS_LEFT = 37;
        public const int KEYS_UP = 38;
        public const int KEYS_RIGHT = 39;
        public const int KEYS_DOWN = 40;
        public const int KEYS_SELECT = 41;
        public const int KEYS_PRINT = 42;
        public const int KEYS_EXECUTE = 43;
        public const int KEYS_PRINTSCREEN = 44;
        public const int KEYS_INSERT = 45;
        public const int KEYS_DELETE = 46;
        public const int KEYS_HELP = 47;
        public const int KEYS_D0 = 48;
        public const int KEYS_D1 = 49;
        public const int KEYS_D2 = 50;
        public const int KEYS_D3 = 51;
        public const int KEYS_D4 = 52;
        public const int KEYS_D5 = 53;
        public const int KEYS_D6 = 54;
        public const int KEYS_D7 = 55;
        public const int KEYS_D8 = 56;
        public const int KEYS_D9 = 57;
        public const int KEYS_A = 65;
        public const int KEYS_B = 66;
        public const int KEYS_C = 67;
        public const int KEYS_D = 68;
        public const int KEYS_E = 69;
        public const int KEYS_F = 70;
        public const int KEYS_G = 71;
        public const int KEYS_H = 72;
        public const int KEYS_I = 73;
        public const int KEYS_J = 74;
        public const int KEYS_K = 75;
        public const int KEYS_L = 76;
        public const int KEYS_M = 77;
        public const int KEYS_N = 78;
        public const int KEYS_O = 79;
        public const int KEYS_P = 80;
        public const int KEYS_Q = 81;
        public const int KEYS_R = 82;
        public const int KEYS_S = 83;
        public const int KEYS_T = 84;
        public const int KEYS_U = 85;
        public const int KEYS_V = 86;
        public const int KEYS_W = 87;
        public const int KEYS_X = 88;
        public const int KEYS_Y = 89;
        public const int KEYS_Z = 90;
        public const int KEYS_LEFTWINDOWS = 91;
        public const int KEYS_RIGHTWINDOWS = 92;
        public const int KEYS_APPS = 93;
        public const int KEYS_SLEEP = 95;
        public const int KEYS_NUMPAD0 = 96;
        public const int KEYS_NUMPAD1 = 97;
        public const int KEYS_NUMPAD2 = 98;
        public const int KEYS_NUMPAD3 = 99;
        public const int KEYS_NUMPAD4 = 100;
        public const int KEYS_NUMPAD5 = 101;
        public const int KEYS_NUMPAD6 = 102;
        public const int KEYS_NUMPAD7 = 103;
        public const int KEYS_NUMPAD8 = 104;
        public const int KEYS_NUMPAD9 = 105;
        public const int KEYS_MULTIPLY = 106;
        public const int KEYS_ADD = 107;
        public const int KEYS_SEPARATOR = 108;
        public const int KEYS_SUBTRACT = 109;
        public const int KEYS_DECIMAL = 110;
        public const int KEYS_DIVIDE = 111;
        public const int KEYS_F1 = 112;
        public const int KEYS_F2 = 113;
        public const int KEYS_F3 = 114;
        public const int KEYS_F4 = 115;
        public const int KEYS_F5 = 116;
        public const int KEYS_F6 = 117;
        public const int KEYS_F7 = 118;
        public const int KEYS_F8 = 119;
        public const int KEYS_F9 = 120;
        public const int KEYS_F10 = 121;
        public const int KEYS_F11 = 122;
        public const int KEYS_F12 = 123;
        public const int KEYS_F13 = 124;
        public const int KEYS_F14 = 125;
        public const int KEYS_F15 = 126;
        public const int KEYS_F16 = 127;
        public const int KEYS_F17 = 128;
        public const int KEYS_F18 = 129;
        public const int KEYS_F19 = 130;
        public const int KEYS_F20 = 131;
        public const int KEYS_F21 = 132;
        public const int KEYS_F22 = 133;
        public const int KEYS_F23 = 134;
        public const int KEYS_F24 = 135;
        public const int KEYS_NUMLOCK = 144;
        public const int KEYS_SCROLL = 145;
        public const int KEYS_LEFTSHIFT = 160;
        public const int KEYS_RIGHTSHIFT = 161;
        public const int KEYS_LEFTCONTROL = 162;
        public const int KEYS_RIGHTCONTROL = 163;
        public const int KEYS_LEFTALT = 164;
        public const int KEYS_RIGHTALT = 165;
        public const int KEYS_BROWSERBACK = 166;
        public const int KEYS_BROWSERFORWARD = 167;
        public const int KEYS_BROWSERREFRESH = 168;
        public const int KEYS_BROWSERSTOP = 169;
        public const int KEYS_BROWSERSEARCH = 170;
        public const int KEYS_BROWSERFAVORITES = 171;
        public const int KEYS_BROWSERHOME = 172;
        public const int KEYS_VOLUMEMUTE = 173;
        public const int KEYS_VOLUMEDOWN = 174;
        public const int KEYS_VOLUMEUP = 175;
        public const int KEYS_MEDIANEXTTRACK = 176;
        public const int KEYS_MEDIAPREVIOUSTRACK = 177;
        public const int KEYS_MEDIASTOP = 178;
        public const int KEYS_MEDIAPLAYPAUSE = 179;
        public const int KEYS_LAUNCHMAIL = 180;
        public const int KEYS_SELECTMEDIA = 181;
        public const int KEYS_LAUNCHAPPLICATION1 = 182;
        public const int KEYS_LAUNCHAPPLICATION2 = 183;
        public const int KEYS_OEMSEMICOLON = 186;
        public const int KEYS_OEMPLUS = 187;
        public const int KEYS_OEMCOMMA = 188;
        public const int KEYS_OEMMINUS = 189;
        public const int KEYS_OEMPERIOD = 190;
        public const int KEYS_OEMQUESTION = 191;
        public const int KEYS_OEMTILDE = 192;
        public const int KEYS_CHATPADGREEN = 202;
        public const int KEYS_CHATPADORANGE = 203;
        public const int KEYS_OEMOPENBRACKETS = 219;
        public const int KEYS_OEMPIPE = 220;
        public const int KEYS_OEMCLOSEBRACKETS = 221;
        public const int KEYS_OEMQUOTES = 222;
        public const int KEYS_OEM8 = 223;
        public const int KEYS_OEMBACKSLASH = 226;
        public const int KEYS_PROCESSKEY = 229;
        public const int KEYS_OEMCOPY = 242;
        public const int KEYS_OEMAUTO = 243;
        public const int KEYS_OEMENLW = 244;
        public const int KEYS_ATTN = 246;
        public const int KEYS_CRSEL = 247;
        public const int KEYS_EXSEL = 248;
        public const int KEYS_ERASEEOF = 249;
        public const int KEYS_PLAY = 250;
        public const int KEYS_ZOOM = 251;
        public const int KEYS_PA1 = 253;
        public const int KEYS_OEMCLEAR = 254;
        

        // mouse buttons -------------------------------------------------------------
        public const int MOUSE_LEFT = 0;
        public const int MOUSE_MIDDLE = 1;
        public const int MOUSE_RIGHT = 2;
        public const int MOUSE_XBUTTON1 = 3;
        public const int MOUSE_XBUTTON2 = 4;

        // gamepad buttons ----------------------------------------------------------
        public const int GAME_PAD_A = 1000;
        public const int GAME_PAD_B = 1001;
        public const int GAME_PAD_X = 1002;
        public const int GAME_PAD_Y = 1003;
        public const int GAME_PAD_BACK = 1004;
        public const int GAME_PAD_BIG_BUTTON = 1005;
        public const int GAME_PAD_START = 1006;
        public const int GAME_PAD_LEFT_SHOULDER = 1007;
        public const int GAME_PAD_LEFT_STICK = 1008;
        public const int GAME_PAD_RIGHT_SHOULDER = 1009;
        public const int GAME_PAD_RIGHT_STICK = 1010;
        public const int GAME_PAD_D_PAD_UP = 1011;
        public const int GAME_PAD_D_PAD_DOWN = 1012;
        public const int GAME_PAD_D_PAD_LEFT = 1013;
        public const int GAME_PAD_D_PAD_RIGHT = 1014;
    }
}