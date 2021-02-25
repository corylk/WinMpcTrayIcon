using System;
using System.Drawing;

namespace WinMpcTrayIcon
{
    public class MenuItem
    {
        public string Text { get; set; }

        public Image Image { get; set; }

        public EventHandler Command { get; set; }

        public MenuItem(string text, Image image, EventHandler command)
        {
            Text = text;
            Image = image;
            Command = command;
        }
    }
}
