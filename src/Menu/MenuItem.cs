using System;
using System.Collections.Generic;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon.Menu
{
    public class MenuItem
    {
        public string Text { get; set; }

        public string Image { get; set; }

        public EventHandler EventHandler { get; set; }

        public List<MenuItem> Items { get; set; }

        public MenuItem(string text, EventHandler eventHandler, Command? command = null)
        {
            Text = text;
            EventHandler = eventHandler;
            Image = command != null ? $"Icons/png/{command}.png" : null;
        }

        public MenuItem(string text, EventHandler eventHandler, bool isActive)
        {
            Text = text;
            EventHandler = eventHandler;
            Image = isActive ? "Icons/png/on.png" : "Icons/png/off.png";
        }

        public MenuItem(string text, List<MenuItem> items)
        {
            Text = text;
            Items = items;
        }
    }
}
