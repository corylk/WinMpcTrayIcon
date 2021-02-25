using System;

namespace WinMpcTrayIcon.Menu
{
    public class SysMenuItem : MenuItem
    {
        public EventHandler EventHandler { get; set; }

        public SysMenuItem(string text, EventHandler eventHandler)
            : base(text)
        {
            Text = text;
            EventHandler = eventHandler;
        }
    }
}
