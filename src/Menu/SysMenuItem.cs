using System;

namespace WinMpcTrayIcon.Menu
{
    public class SysMenuItem : MenuItem
    {
        public EventHandler Command { get; set; }

        public SysMenuItem(string text, EventHandler command)
            : base(text)
        {
            Text = text;
            Command = command;
        }
    }
}
