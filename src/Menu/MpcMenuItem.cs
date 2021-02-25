using System;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Menu
{
    public class MpcMenuItem : SysMenuItem
    {
        public string Image { get; set; }

        public MpcMenuItem(string text, EventHandler eventHandler, Command? command = null)
            : base(text, eventHandler)
        {
            Text = text;
            EventHandler = eventHandler;
            Image = command != null ? $"Icons/png/{command}.png" : null;
        }

        public MpcMenuItem(string text, EventHandler eventHandler, bool isActive)
            : base(text, eventHandler)
        {
            Text = text;
            EventHandler = eventHandler;
            Image = isActive ? "Icons/png/on.png" : "Icons/png/off.png";
        }
    }
}
