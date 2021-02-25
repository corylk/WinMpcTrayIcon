using System;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Menu
{
    public class SwitchMenuItem : SysMenuItem
    {
        public string Image { get; set; }

        public SwitchMenuItem(string text, EventHandler eventHandler, bool isActive)
            : base(text, eventHandler)
        {
            Text = text;
            EventHandler = eventHandler;
            Image = isActive ? "Icons/png/on.png" : "Icons/png/off.png";
        }
    }
}
