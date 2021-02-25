using System.Collections.Generic;

namespace WinMpcTrayIcon.Menu
{
    public class GroupMenuItem : MenuItem
    {
        public List<MenuItem> Items { get; set; }

        public GroupMenuItem(string text, List<MenuItem> items)
            : base(text)
        {
            Text = text;
            Items = items;
        }
    }
}
