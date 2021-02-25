using System;
using System.Drawing;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class MenuItem
    {
        public string Text { get; set; }

        public Command Command { get; set; }

        public bool HasImage { get; set; }

        public MenuItem(string text, Command command, bool hasImage)
        {
            Text = text;
            Command = command;
            HasImage = hasImage;
        }
    }
}
