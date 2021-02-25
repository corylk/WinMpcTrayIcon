using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class MenuItem
    {
        public string Text { get; set; }

        public Command Command { get; set; }

        public bool HasImage { get; set; }

        public MenuItem(string text, Command? command = null, bool hasImage = false)
        {
            Text = text;
            Command = command ?? Command.status;
            HasImage = hasImage;
        }
    }
}
