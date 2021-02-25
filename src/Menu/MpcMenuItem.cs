using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Menu
{
    public class MpcMenuItem : MenuItem
    {
        public Command Command { get; set; }

        public bool HasImage { get; set; }

        public MpcMenuItem(string text, Command command, bool hasImage = false)
            : base(text)
        {
            Text = text;
            Command = command;
            HasImage = hasImage;
        }
    }
}
